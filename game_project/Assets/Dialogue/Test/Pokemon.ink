INCLUDE globals.ink
-> main

=== main === 
Which pokemon do you choose?
    +[Charmander]
        -> chosen("Charmander")
    +[Bulbasaur]
        -> chosen("Bulbasaur")
    +[Squirtle]
        -> chosen("Squirtle")

=== chosen(pokemon) ===
~pokemon_name = pokemon
You chose {pokemon}!
-> END
=== already_chose === 
You already chose {pokemon_name}
-> END