INCLUDE globals.ink
EXTERNAL PlayDragonCut()

//First is plowing, second is market, third is next day.
EXTERNAL ShowFirstQuest() 
EXTERNAL DoNotShowFirstQuest()

EXTERNAL ShowSecondQuest()
EXTERNAL DoNotShowSecondQuest()

EXTERNAL ShowThirdQuest()
EXTERNAL DoNotShowThirdQuest()

-> main

/* Variable declarations
VAR visited_elara = false
VAR asked_about_town = false
VAR asked_about_farming = false
VAR both_quests_done = false
*/
=== main ===
{  
    // Check if the player has visited Elara before
    - (visited_elara == 1):
        -> elara_follow_up
    - else:
        -> elara_initial
}

=== elara_initial ===
Ah, there you are… Welcome, welcome! I’m Elara, and I’m so glad to see you arrive safely in Silverbrook.  #speaker:Elara #portrait:elaraSmiling

You must have been on quite a journey to be gone so long. #speaker:Elara #portrait:elaraSmiling

//(Yes, it’s been quite a while.)

+ [Could you tell me what’s happened in the town the last few years?]
    -> town_update
+ [I’m quite ready to get started with farming. Any advice?]
    -> farming_advice

=== town_update ===
//For the quest panel!
~ ShowSecondQuest()
Of course! Well, this is Silverbrook, same old town. Unicorn forest to the south has seen quite more activity the past few years than before, and Dragon Hill up north is still as imposing as ever — careful with your farm, there. #speaker:Elara  #portrait:elaraSerious

~ DoNotShowFirstQuest()
We pride ourselves on our resilience and cheerful townsfolk, even with those dragon sightings. And of course, we farm the finest crops this side of the kingdom!#speaker:Elara  #portrait:elaraSmiling

+ [That sounds wonderful! I’m excited to explore.]

That’s great enthusiasm. You really should go check out the Market square, over yonder bridge. You’ve been given some silver coins, go ahead and spend some of those.#speaker:Elara #portrait:elaraSmiling

//+ [I could probably use some new equipment.]

Come back once you’ve done that, and I can show you around the farm!#speaker:Elara #portrait:elaraSerious

    
    // Remember that the player has visited Elara and asked about the town
    ~ visited_elara = 1
    ~ asked_about_town = 1


{  

    // Check if both quests are done
    - (asked_about_farming == 1):
        ~ both_quests_done = 1
}

-> END

=== farming_advice ===
    //For the quest panel!
    ~ ShowFirstQuest()
You’re ready for farming? Here, I’ll show you around.#speaker:Elara  #portrait:elaraLooking

~ DoNotShowSecondQuest()
Use the plow to break up the land. This readies it for planting the seeds.#speaker:Elara  #portrait:elaraSerious

+ [How do I harvest the crops?]

Good question! They’re not ready to harvest yet, but when they are in the future, I will show you.#speaker:Elara  #portrait:elaraSmiling

Oh! I almost forgot to say, look around town for the hoe tool. I had it but must have dropped it on the way here...sorry.#speaker:Elara  #portrait:elaraSad

    // Remember that the player has visited Elara and asked about farming
    ~ visited_elara = 1
    ~ asked_about_farming = 1
{  
    // Check if both quests are done
    - (asked_about_town == 1):
        ~ both_quests_done = 1
}

-> END

//--------------------------------------------------------------------------------
=== elara_follow_up ===
{  
    // Check if both quests are done
    - both_quests_done == 1 && nextDay == 0:
        -> both_quests_done_knot
    - both_quests_done == 1 && nextDay == 1:
        -> next_day
    - {
        // Check the player's previous choice
        - asked_about_town == 1:
            -> farming_advice_follow_up
        - asked_about_farming == 1:
            -> town_update_follow_up
    }
}

=== town_update_follow_up ===
Oh, welcome back! Did you learn to plow and are ready to explore Silverbrook further?#speaker:Elara #portrait:elaraSerious

+ [I did all thanks to you! I am ready!]
 ->  town_update

+ [No, I need guidance.]
Let's go over the plowing process again, just to make sure you've got it. Use the plow. Try using the map if you cannot find it.#speaker:Elara  #portrait:elaraSerious

-> END

=== farming_advice_follow_up ===
Ah, back already! Have you bought some seeds?#speaker:Elara  #portrait:elaraLooking

+ [Yes.]
    -> farming_advice
+ [No.]
Remember, the Market square is a good place to start!#speaker:Elara  #portrait:elaraSmiling

-> END
//---------------------------------------------------------------------------------
=== both_quests_done_knot ===
    //For the quest panel!
    ~ ShowThirdQuest()
Don’t forget to come back tomorrow morning to check up on the farm.#speaker:Elara  #portrait:elaraSmiling

// Set next day dialogue to occur the next time the player walks up
~ nextDay = 1
-> END
//-> next_day

=== next_day ===
~ DoNotShowThirdQuest()
Good morning! I hope you had a good first day at the farm yesterday and rested well. We’ve got lots of things to … #speaker:Elara  #portrait:elaraLooking

~ PlayDragonCut()

<b>Oh no. I hoped we wouldn’t see the dragon again so soon.</b>#speaker:Elara  #portrait:elaraScared

+ [Again? Is this a common occurrence?]

Yeah… But the unicorns can help save your harvest. I’ll uhh… tell you more when the dragon is gone. I’ve got to go!#speaker:Elara  #portrait:elaraSad

-> END


