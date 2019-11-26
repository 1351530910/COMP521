AI:
every iteration has one target (first spice with less than 2 in caravan), 
from the easiest to get(turmeric) to the hardest to get. 
	-if player got a sumac and then got stolen, it will waste a lot of time
It will trade multiple times, and to to caravan once the target spice is in the inventory.
If player is intercepted by thief, it will deposit the current inventory to the caravan.
how to get each type of spice is predefined in code. 
the predefined routine is NOT optimal and will have some useless trades. but guaranteed to have the target spice when finished.

thief:
every 5 seconds, 33% chance to steal with 50% chance player/caravan. chance of stealing all types of spice are equal.

since plus is not working on my machine, up and down arrow are also bound to increase/decrease simulation speed.
(in ideal case, plus and up arrow will work same way, minus and down arrow work same way)

if the simulation speed is too high, it is possible that there will be concurrency issue with trading.
with a decent machine, the X2 simulation speed should work fine. (in my case it worked below X8 speed)



