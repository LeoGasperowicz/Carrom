# Game Project 
Here it is my repo for my Carrom Game.

## Carrom Rules:

Each player has 9 pieces of the same color. There is a queen (red piece) in the middle.

### Objective of the game:
To pocket one's pieces into the holes using the striker, which is launched from a defined area. (All pieces are worth 1 point, and the queen is worth 5 points.)

### End of the game:
The game ends when a player has scored 20 points or if all pieces of one color have been pocketed.

### Penalties:
For any penalty, one of the player's previously pocketed pieces is brought out and placed in the middle of the board, and the player loses their turn:
- If a player pockets the queen and does not immediately pocket one of their own pieces afterward, the queen is also placed back in the middle.
- If the striker is pocketed directly.

### Special cases:
- If a player pockets an opponent's piece, it awards a piece to the opponent, but they retain the turn.
- If the striker is pocketed at the same time as an opponent's piece, the opponent scores a point, and a piece from the current player is brought out.
- If the player pockets their own piece and the striker in the same shot, they score a point, but their piece is brought out, and their turn ends.
- The player who pockets the last piece of a color while the Queen is still in play loses the game, and the other player gains 5 additional points.


## Description of the Code
You could find eleven classes that I created. Three of them are for the connection to the database and they are together in a specific folder.

The main class is Game this is where is implement all the logic for my game, with all the elements I need to have a functional game.
Inside you will find : 
 - attributs
 - InitializeGame : This method as the name suggests serve to  create all the elements we need for the game (2 players, a bord with specific dimensions, a list of pawns for each player, a striker, a score, the queen (the pawn with which you can win a maximum of points) and decide which player starts).
 -  Play : This method is the basics for how the game works. Thanks to different methods created in different classes we get some information like how the player want to send the striker on the board with the method named GetStrikerDirectionFromUser created in the class game where we are. I didn't fill this mezthod because for me it will be related to the WPF because I want to give the posibility to move the striker in a specific zone thanks to the right click of the mouse and with the left click he will be able to give a specific direction and speed to the striker. In this method Play you have the method isGameOver one more time as the name suggests, this is to verify is the game is over or not with some conditions (the number of points of a player or if a player has not more pawns). Then in this method we just update the scores, verify the current player scores and see which are the pawns in the holes. Also, we verifiy a specific condition to get the five points with the queen as you can see in the rules above.

The other class which need a bit more of explications is Board : 
In this class I created methods to try to implement the physics into the game with : the reaction of the pawns when they collide and against the edge and also to know if they are in a hole or not.
