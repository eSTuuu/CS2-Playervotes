<div align="center">
  <img src="https://i.ibb.co/qQRyk5T/CS2-Playervotes2.png" width="500"/>
  <h3>🎮 CS2 Playervotes</h3>
  <p>Lightweight and efficient voting system for CS2 without anything pointless, allowing players to initiate votes for kicking, banning, and muting players. 
  <br>The plugin ensures fair play by requiring a configurable percentage of votes to pass before an action is taken.
  <br>Plugin uses commands like css_ban, css_kick, css_mute so actions are stored into your site that shows punishments.</p>
</div>
<div align="center">
  <img src="https://img.shields.io/github/v/tag/asapverneri/CS2-Playervotes?style=for-the-badge&label=Version" alt="GitHub tag (with filter)" />
  <img src="https://img.shields.io/github/last-commit/asapverneri/CS2-Playervotes?style=for-the-badge" alt="Last Commit" />
  <blockquote>
    <strong>⚠️ <span style="color:red;">CAUTION</span></strong>  
    <br><span style="color:red;">This plugin should work with almost any admin plugin.</span>
  </blockquote>
</div>

---

## 📋 Features / Roadmap

<p>✅ Votekick</p>
<p>✅ Voteban</p>
<p>✅ Votemute</p>
<p>✅ Votegag</p>
<p>✅ Votesilence</p>
<p>✅ Immunity for VIPs</p>
<p>✅ Configurable Vote Threshold</p>
<p>✅ Easy usage</p>
<br>
<p>⬜ ...</p>

---

## 📦 Installion

- Install latest [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) & [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
- Install all requirements listed below.
- Download the latest release from the releases tab and copy it into the csgo folder.

**Requirements**
- [MenuManager](https://genesis-cs.space/menuconstructor/menumanager/api/index.html#start) (`MenuManagerApi.dll` in `addons/counterstrikesharp/shared/MenuManagerApi/`). The player picks the menu type with `!menu`.

**Example config:**
```json
{
  "RequiredVotePercentage": 80,
  "RequiredPlayers": 2,    // Min players to allow playervotes
  "ClearVotesOnMapStart": true,
  "Line": "----------------------------------",
  "Votemute": true,
  "PermissionForVoteMute": "",    // "#css/vip" or "@css/vip" for example
  "MuteDuration": 60,
  "MuteReason": "Votemuted",
  "Line2": "----------------------------------",
  "Voteban": true,
  "PermissionForVoteBan": "",
  "BanDuration": 120,
  "BanReason": "Votebanned",
  "Line3": "----------------------------------",
  "Votekick": true,
  "PermissionForVoteKick": "",
  "KickReason": "Votekicked",
  "Line4": "----------------------------------",
  "Votegag": true,
  "PermissionForVoteGag": "",
  "GagDuration": 60,
  "GagReason": "Votegagged",
  "Line5": "----------------------------------",
  "Votesilence": true,
  "PermissionForVoteSilence": "",
  "SilenceDuration": 60,
  "SilenceReason": "Votesilenced",
  "Line6": "----------------------------------",
  "VoteImmunity": "@css/vip, @css/vvip",
  "ConfigVersion": 1
}
```

---

## ⌨️ Commands
| Command         | Description                                                          | Permissions |
|-----------------|----------------------------------------------------------------------|-------------|
| !votemute       | Command for listing players and voting for mute                      | -           |
| !voteban        | Command for listing players and voting for ban                       | -           |
| !votekick       | Command for listing players and voting for kick                      | -           |
| !votegag        | Command for listing players and voting for gag                       | -           |
| !votesilence    | Command for listing players and voting for silence                   | -           |

---

## 📫 Contact

<div align="center">
  <a href="https://discordapp.com/users/367644530121637888">
    <img src="https://img.shields.io/badge/Discord-7289DA?style=for-the-badge&logo=discord&logoColor=white" alt="Discord" />
  </a>
  <a href="https://steamcommunity.com/id/vvernerii/">
    <img src="https://img.shields.io/badge/Steam-000000?style=for-the-badge&logo=steam&logoColor=white" alt="Steam" />
  </a>
</div>

---

## 💖 Support My Work

<div align="center">
  <a href="https://www.paypal.com/paypalme/PeliluolaCS2">
    <img src="https://img.shields.io/badge/Donate-PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white" alt="Donate via PayPal" />
  </a>
  <a href="https://buy.stripe.com/cN2dThbavflW05G7sz">
    <img src="https://img.shields.io/badge/Donate-Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white" alt="Donate via Stripe" />
  </a>
</div>
