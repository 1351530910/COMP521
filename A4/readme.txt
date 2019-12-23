
to match real life logic, if someone goes to a seat and that seat is occupied by someone else, 
	he just go to another seat instead of waiting for the dude to finish eating. repeat if same happens

agent's velocity is computed using an equilibrium between flee and seek steering force. 
flee steering force is in aaura.cs and aura.cs, 
	it is inside OnTriggerStay because i think that object that are too far should not contribute to fleeing force
seek steering force in navigator.cs and advertiser.cs.

if advertiser is stick to a person he will follow that person for 4 seconds.
if multiple advertiser arrives at the same person only one of them will remain.
if a person is retained by an advisor, it is possible that the person will stop a little bit and talk with advertisor.
	but the total time will stay 4 seconds as mentioned in the handout.
