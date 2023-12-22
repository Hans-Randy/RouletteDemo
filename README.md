<center>
<img src="/static/assets/img/bg.jpg" />

# REST Roulette

</center>

## Stack
- <b>.NET 7</b>
- <b>.Entity Framework 7</b>
- <b>Sqlite</b>


## Usage

### Play game
<pre><b>POST</b>/api/roulette/placebet</pre>
<pre><b>POST</b>/api/roulette/spin</pre>
<pre><b>GET</b>/api/roulette/payout</pre>
<pre><b>GET</b>/api/roulette/showpreviouspins</pre>

- <b>PlaceBet:</b> Allows users to place a bet.
- <b>Spin:</b> Generates a random number between 0 and 36.
- <b>Payout:</b> Determines whether a player has won based on their previous bets and the latest spin result.
- <b>ShowPreviousSpins:</b> Displays a list of previous spin results.

## Bets and Payouts
This API supports the following bets:
- <b>Number:</b> 36 to 1

Bet on a number (Payout 35 times the amount)


## Development
Version <b>1.0.0</b>

Written by <b>Hans-Randy Masamba</b>
