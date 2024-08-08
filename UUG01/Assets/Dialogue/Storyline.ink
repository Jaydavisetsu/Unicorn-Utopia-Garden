INCLUDE globals.ink
EXTERNAL PlayDragonCut()

EXTERNAL ShowFirstQuest()
//EXTERNAL DoNotShowFirstQuest()

EXTERNAL ShowSecondQuest()
//EXTERNAL DoNotShowSecondQuest()

EXTERNAL ShowThirdQuest()
//EXTERNAL DoNotShowThirdQuest()

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
    - (visited_elara == true):
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
~ ShowFirstQuest()
Of course! Well, this is Silverbrook, same old town. Unicorn forest to the south has seen quite more activity the past few years than before, and Dragon Hill up north is still as imposing as ever — careful with your farm, there. #speaker:Elara  #portrait:elaraSerious

We pride ourselves on our resilience and cheerful townsfolk, even with those dragon sightings. And of course, we farm the finest crops this side of the kingdom!#speaker:Elara  #portrait:elaraSmiling

+ [That sounds wonderful! I’m excited to explore.]

That’s great enthusiasm. You really should go check out the Market square, over yonder bridge. You’ve been given some silver coins, go ahead and spend some of those.#speaker:Elara #portrait:elaraSmiling

//+ [I could probably use some new equipment.]

Come back once you’ve done that, and I can show you around the farm!#speaker:Elara #portrait:elaraSerious

    
    // Remember that the player has visited Elara and asked about the town
    ~ visited_elara = true
    ~ asked_about_town = true


{  

    // Check if both quests are done
    - (asked_about_farming == true):
        ~ both_quests_done = true
}

-> END

=== farming_advice ===
    //For the quest panel!
    ~ ShowSecondQuest()
You’re ready for farming? Here, I’ll show you around.#speaker:Elara  #portrait:elaraLooking

Use the plow to break up the land. This readies it for planting the seeds.#speaker:Elara  #portrait:elaraSerious

+ [How do I harvest the crops?]

Good question! They’re not ready to harvest yet, but when they are in the future, I will show you.#speaker:Elara  #portrait:elaraSmiling

Oh! I almost forgot to say, look around town for the hoe tool. I had it but must have dropped it on the way here...sorry.#speaker:Elara  #portrait:elaraSad

    // Remember that the player has visited Elara and asked about farming
    ~ visited_elara = true
    ~ asked_about_farming = true
{  
    // Check if both quests are done
    - (asked_about_town == true):
        ~ both_quests_done = true
}

-> END

//--------------------------------------------------------------------------------
=== elara_follow_up ===
{  
    // Check if both quests are done
    - both_quests_done == true:
        -> both_quests_done_knot
        
    - {
        // Check the player's previous choice
        - asked_about_town == true:
            -> farming_advice_follow_up
        - asked_about_farming == true:
            -> town_update_follow_up
    }
}

=== town_update_follow_up ===
Oh, welcome back! Ready to explore Silverbrook further?#speaker:Elara #portrait:elaraSerious

+ [Absolutely!]

Remember, the Market square is a good place to start!#speaker:Elara  #portrait:elaraSmiling

-> END

=== farming_advice_follow_up ===
Ah, back for more farming tips, I see!#speaker:Elara  #portrait:elaraLooking

+ [Yes, I'm eager to get started.]

Let's go over the plowing process again, just to make sure you've got it. Use the plow.#speaker:Elara  #portrait:elaraSerious

-> END
//---------------------------------------------------------------------------------
=== both_quests_done_knot ===
    //For the quest panel!
    ~ ShowThirdQuest()
Don’t forget to come back tomorrow morning to check up on the farm.#speaker:Elara  #portrait:elaraSmiling

-> next_day

=== next_day ===
Good morning! I hope you had a good first day at the farm yesterday and rested well. We’ve got lots of things to … #speaker:Elara  #portrait:elaraLooking

// [dragon roar]

<b>Oh no. I hoped we wouldn’t see the dragon again so soon.</b>#speaker:Elara  #portrait:elaraScared

+ [Again? Is this a common occurrence?]

Yeah… But the unicorns can help save your harvest. I’ll uhh… tell you more when the dragon is gone. I’ve got to go!#speaker:Elara  #portrait:elaraSad

-> END


