using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Menu;
using MenuManager;
using Microsoft.Extensions.Logging;

namespace Playervotes;

public class Playervotes : BasePlugin, IPluginConfig<PlayervotesConfig>
{
    public override string ModuleName => "Playervotes";
    public override string ModuleDescription => "Lightweight playervotes for cs2";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "2.5";

    public PlayervotesConfig Config { get; set; } = new();

    private readonly Dictionary<ulong, HashSet<ulong>> voteKickCounts = new();
    private readonly Dictionary<ulong, HashSet<ulong>> voteBanCounts = new();
    private readonly Dictionary<ulong, HashSet<ulong>> voteMuteCounts = new();
    private readonly Dictionary<ulong, HashSet<ulong>> voteGagCounts = new();
    private readonly Dictionary<ulong, HashSet<ulong>> voteSilenceCounts = new();

    private static readonly PluginCapability<IMenuApi?> MenuApi = new("menu:nfcore");
    private IMenuApi? _menu;

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        try { _menu = MenuApi.Get(); }
        catch { _menu = null; }

        if (_menu == null)
            Logger.LogError("MenuManager API (menu:nfcore) is not available. Player vote menus will not open.");
    }

    public void OnConfigParsed(PlayervotesConfig config)
    {
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        Logger.LogInformation($"Loaded (version {ModuleVersion})");

        RegisterListener<Listeners.OnMapStart>(OnMapStart);

        if (Config.EnableVotekick)
            AddCommand("css_votekick", "Start votekick", OnVotekick);

        if (Config.EnableVoteban)
            AddCommand("css_voteban", "Start voteban", OnVoteban);

        if (Config.EnableVotemute)
            AddCommand("css_votemute", "Start votemute", OnVotemute);

        if (Config.EnableVotegag)
            AddCommand("css_votegag", "Start votegag", OnVotegag);

        if (Config.EnableVotesilence)
            AddCommand("css_votesilence", "Start votesilence", OnVotesilence);
    }
    private void OnMapStart(string map)
    {
        if (Config.ClearVotesOnMapStart)
        {
            voteKickCounts.Clear();
            voteBanCounts.Clear();
            voteMuteCounts.Clear();
            voteGagCounts.Clear();
            voteSilenceCounts.Clear();
        }
    }
    public void OnVotekick(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid)
            return;

        if (!EnoughPlayers())
        {
            player.PrintToChat($"{Localizer["noenoughplayers", Config.RequiredPlayers]}");
            return;
        }

        if (!string.IsNullOrEmpty(Config.PermissionForVoteKick))
        {
            if (Config.PermissionForVoteKick.StartsWith("#"))
            {
                if (!AdminManager.PlayerInGroup(player, Config.PermissionForVoteKick))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
            else if (Config.PermissionForVoteKick.StartsWith("@"))
            {
                if (!AdminManager.PlayerHasPermissions(player, Config.PermissionForVoteKick))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
        }

        OpenPlayerMenu(player, $"{Localizer["votekick.title"]}", "kick");
    }

    public void OnVoteban(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid)
            return;

        if (!EnoughPlayers())
        {
            player.PrintToChat($"{Localizer["noenoughplayers", Config.RequiredPlayers]}");
            return;
        }

        if (!string.IsNullOrEmpty(Config.PermissionForVoteBan))
        {
            if (Config.PermissionForVoteBan.StartsWith("#"))
            {
                if (!AdminManager.PlayerInGroup(player, Config.PermissionForVoteBan))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
            else if (Config.PermissionForVoteBan.StartsWith("@"))
            {
                if (!AdminManager.PlayerHasPermissions(player, Config.PermissionForVoteBan))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
        }

        OpenPlayerMenu(player, $"{Localizer["voteban.title"]}", "ban");
    }

    public void OnVotemute(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid)
            return;

        if (!EnoughPlayers())
        {
            player.PrintToChat($"{Localizer["noenoughplayers", Config.RequiredPlayers]}");
            return;
        }

        if (!string.IsNullOrEmpty(Config.PermissionForVoteMute))
        {
            if (Config.PermissionForVoteMute.StartsWith("#"))
            {
                if (!AdminManager.PlayerInGroup(player, Config.PermissionForVoteMute))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
            else if (Config.PermissionForVoteMute.StartsWith("@"))
            {
                if (!AdminManager.PlayerHasPermissions(player, Config.PermissionForVoteMute))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
        }

        OpenPlayerMenu(player, $"{Localizer["votemute.title"]}", "mute");
    }

    public void OnVotegag(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid)
            return;

        if (!EnoughPlayers())
        {
            player.PrintToChat($"{Localizer["noenoughplayers", Config.RequiredPlayers]}");
            return;
        }

        if (!string.IsNullOrEmpty(Config.PermissionForVoteGag))
        {
            if (Config.PermissionForVoteGag.StartsWith("#"))
            {
                if (!AdminManager.PlayerInGroup(player, Config.PermissionForVoteGag))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
            else if (Config.PermissionForVoteGag.StartsWith("@"))
            {
                if (!AdminManager.PlayerHasPermissions(player, Config.PermissionForVoteGag))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
        }

        OpenPlayerMenu(player, $"{Localizer["votegag.title"]}", "gag");
    }
    public void OnVotesilence(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid)
            return;

        if (!EnoughPlayers())
        {
            player.PrintToChat($"{Localizer["noenoughplayers", Config.RequiredPlayers]}");
            return;
        }

        if (!string.IsNullOrEmpty(Config.PermissionForVoteSilence))
        {
            if (Config.PermissionForVoteSilence.StartsWith("#"))
            {
                if (!AdminManager.PlayerInGroup(player, Config.PermissionForVoteSilence))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
            else if (Config.PermissionForVoteSilence.StartsWith("@"))
            {
                if (!AdminManager.PlayerHasPermissions(player, Config.PermissionForVoteSilence))
                {
                    player.PrintToChat($"{Localizer["noaccess"]}");
                    return;
                }
            }
        }

        OpenPlayerMenu(player, $"{Localizer["votesilence.title"]}", "silence");
    }

    private void StartVote(CCSPlayerController voter, CCSPlayerController target, string action)
    {
        if (target.SteamID == voter.SteamID)
        {
            voter.PrintToChat($"{Localizer["voteurself"]}");
            return;
        }

        string[] flags = Config.VoteImmunity.Split(',').Select(flag => flag.Trim()).ToArray();
        foreach (var flag in flags)
        {
            if (flag.StartsWith("#"))
            {
                if (AdminManager.PlayerInGroup(target, flag))
                {
                    voter.PrintToChat($"{Localizer["cantvote"]}");
                    return;
                }
            }
            else if (flag.StartsWith("@"))
            {
                if (AdminManager.PlayerHasPermissions(target, flag))
                {
                    voter.PrintToChat($"{Localizer["cantvote"]}");
                    return;
                }
            }
        }
        var voteDictionary = action switch
        {
            "ban" => voteBanCounts,
            "mute" => voteMuteCounts,
            "kick" => voteKickCounts,
            "gag" => voteGagCounts,
            "silence" => voteSilenceCounts,
            _ => throw new ArgumentException("Invalid action")
        };

        if (!voteDictionary.ContainsKey(target.SteamID))
        {
            voteDictionary[target.SteamID] = new HashSet<ulong>();
        }

        if (voteDictionary[target.SteamID].Contains(voter.SteamID))
        {
            voter.PrintToChat($"{Localizer["alreadyvoted", voter.PlayerName, action, target.PlayerName]}");
            return;
        }

        voteDictionary[target.SteamID].Add(voter.SteamID);

        int currentVotes = voteDictionary[target.SteamID].Count;
        int requiredVotes = (int)System.Math.Ceiling((Config.RequiredVotePercentage / 100) * GetActivePlayers().Count);
        Server.PrintToChatAll($"{Localizer["voteprogress", voter.PlayerName, action, target.PlayerName, currentVotes, requiredVotes]}");

        if (currentVotes >= requiredVotes)
        {
            if (action == "ban")
            {
                Server.PrintToChatAll($"{Localizer["votebanned", target.PlayerName, Config.BanDuration]}");
                Server.ExecuteCommand($"css_ban #{target.UserId} {Config.BanDuration} \"{Config.BanReason}\"");
            }
            else if (action == "mute")
            {
                Server.PrintToChatAll($"{Localizer["votemuted", target.PlayerName, Config.MuteDuration]}");
                Server.ExecuteCommand($"css_mute #{target.UserId} {Config.MuteDuration} \"{Config.MuteReason}\"");
            }
            else if (action == "kick")
            {
                Server.PrintToChatAll($"{Localizer["votekicked", target.PlayerName]}");
                Server.ExecuteCommand($"css_kick #{target.UserId} \"{Config.KickReason}\"");
            }
            else if (action == "gag")
            {
                Server.PrintToChatAll($"{Localizer["votegagged", target.PlayerName, Config.GagDuration]}");
                Server.ExecuteCommand($"css_gag #{target.UserId} {Config.GagDuration} \"{Config.GagReason}\"");
            }
            else if (action == "silence")
            {
                Server.PrintToChatAll($"{Localizer["votesilenced", target.PlayerName, Config.SilenceDuration]}");
                Server.ExecuteCommand($"css_silence #{target.UserId} {Config.SilenceDuration} \"{Config.SilenceReason}\"");
            }
            voteDictionary.Remove(target.SteamID);
        }
    }
    private static List<CCSPlayerController> GetActivePlayers()
    {
        return Utilities.GetPlayers()
            .Where(p => !p.IsHLTV && !p.IsBot && p.PlayerPawn.IsValid && p.Connected == PlayerConnectedState.Connected)
            .ToList();
    }
    private bool EnoughPlayers()
    {
        int activePlayersCount = GetActivePlayers().Count;
        return activePlayersCount >= Config.RequiredPlayers;
    }

    private void OpenPlayerMenu(CCSPlayerController player, string title, string action)
    {
        if (_menu == null)
        {
            player.PrintToChat($"{Localizer["nomenu"]}");
            return;
        }

        IMenu menu = _menu.GetMenu(title);
        menu.PostSelectAction = PostSelectAction.Close;

        foreach (var target in GetActivePlayers())
        {
            var voted = target;
            menu.AddMenuOption(voted.PlayerName, (client, _) =>
            {
                StartVote(client, voted, action);
            });
        }

        menu.Open(player);
    }
}