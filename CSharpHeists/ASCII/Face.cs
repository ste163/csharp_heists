//ASCII CREDITS(I TAKE NO CREDIT FOR ANY ARTWORK.I did modify most artwork to fit the screen better)
    // https://textfancy.com/multiline-text-art/
    // https://asciiart.website/
    // https://asciiart.website/index.php?art=people/faces
    // https://simpletextart.blogspot.com/2013/12/money-bag-picture-made-with-ascii-text.html
    // https://www.asciiart.eu/buildings-and-places/cities
    // http://www.asciiworld.com/-Buildings-.html

using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpHeists.ASCII
{
    public class Face
    {
            public static string GenerateCriminalFace(int r)
            {
                string face = "";

                switch (r)
                {
                    case 1:
                        face = @"
  \\\\\\\|
 | _   _ |       
( (o) (o) )     
 |  . .  |           
  \  _  /         
   \___/";
                        break;
                    case 2:
                        face = @"
  |||||   
{. @ @ .} 
 |  o  |        
  \_U_/";
                        break;
                    case 3:
                        face = @"
  .xxxx. 
 |(o)(o)|
(  (__)  )
  | __ |
   \__/ ";
                        break;
                    case 4:
                        face = @"
    _____
  /`---'- \
 (_    _ \_\
 `o'/ 'o` 7)
  \`-     |
   \`~'  /\
    `---'  ";
                        break;
                    case 5:
                        face = @"
    ______
 /  `       \
| __   ~~~~ |
\   \__  -. |
( o( o)---/ /
/  /     -|/
\  =   /  |
(_____/   |";
                        break;
                    case 6:
                        face = @"
   .-```'.
  /   \    \
 /   / `\__/
| .'  _  _|
\(\   6  6
 | \   _\ |
 |\   `= `/
   '.___.'";
                        break;
                    case 7:
                        face = @"
      _.===.
   _,/_ \\\ `-.
  /`/| /      _\
 // ` /////////\
/`/` /|<o>)(o>( 
\  \/\|    /  /
 |`/` /\ `=' /
 |/\/`  `._.'";
                        break;
                }

                return face;
            }

            public static string GenerateCriminalFaceIced(int r)
            {
                string face = "";

                switch (r)
                {
                    case 1:
                        face = @"
  \\\\\\\|   
 | _   _/ *      
( (X)(X( *   * 
 |  . . \ *          
  \  o  /    *      
   \___/";
                        break;
                    case 2:
                        face = @"
  |||||/*   
{. X X( * *
 |  o  \ *       
  \_O_/";
                        break;
                    case 3:
                        face = @"
  .xxxx. 
 |(X)(X/*
(  (_( * *
  | O \ *   *
   \__/ ";
                        break;
                    case 4:
                        face = @"
    _____
  /`---'- \
 (-    -  / *
 `X / X  ( *  * 
  \`-     \ * *
   \ O   /\     *
    `---'  ";
                        break;
                    case 5:
                        face = @"
    ______
 /  `     /
| __   ~~/ * *
\   \__ (* **
( X( X)--\ *
/  /     |  *
\  O   / \     *
(_____/   \";
                        break;
                    case 6:
                        face = @"
   .-```'.   *
  /     /* *
 /   /  \* *
| .'  _  \ *  * 
\(\   X  X    *
 | \   _\ |
 |\    O  /
   '.___.'";
                        break;
                    case 7:
                        face = @"
      _.===.
   _,/_ \\\ `\
  /`/| /     /  *
 // ` //////(*  *
/`/` /| X  X(* *  
\  \/\|    / \* *
 |`/`/\  O  /    *
 |/\/` `._.'";
                        break;
                }

                return face;
            }

            public static string DisplayArrested()
            {
                return @"==============================
||     ||<(.)>||<(.)>||     || 
||    _||     ||     ||_    || 
||   (__D     ||     C__)   || 
||   (__D     ||     C__)   ||
||   (__D     ||     C__)   ||
||   (__D     ||     C__)   ||
||     ||     ||     ||     ||
==============================
";
            }
        }
    }

    // When I add guards, show this message. The muscle will have to deal with this punk
    //                       ________________
    //                       \      __      /         __
    //                        \_____()_____/         /  )
    //                        '============`        /  /
    //                         #---\  /---#        /  /
    //                        (# @\| |/@  #)      /  /
    //                         \   (_)   /       /  /
    //                         |\ '---` /|      /  /
    //                 _______/ \\_____// \____/ o_|
    //                /       \  /     \  /   / o_|
    //               / |           o|        / o_| \
    //              /  |  _____     |       / /   \ \
    //             /   |  |===|    o|      / /\    \ \
    //            |    |   \@/      |     / /  \    \ \
    //            |    |___________o|__/----)   \    \/
    //            |    '              ||  --)    \     |
    //            |___________________||  --)     \    /
    //                 |           o|   ''''   |   \__/
    //                 |            |          |

    //                   "DON'T CROSS ME... !"
    // Rosebud

