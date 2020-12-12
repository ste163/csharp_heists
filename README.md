# C# Heists
>Text-based heist adventure game Windows, Linux, and Mac terminals

![C# Heists overview GIF](/readme_overview.gif)

## Features
- manage a crew of untrustworthy criminals
    - hire associates
    - ice unruly associates
    - manage morale (unhappy associates may turn against you!)
- perform five heists throughout Nashville
    - stakeout locations to see if they become more or less difficult
- six endings based on how much money you got away with, whether you survived, or got arrested
- enough randomization so no two playthroughs have the same outcome

## Known Issues
- Pressing ```enter``` multiple times during a heist countdown will skip heist summary
- Text distortion if terminal window is not wide enough. Maximizing window fixes this
- Background color may not resize correctly on Linux. Restarting game with a maximized window fixes this

## Setup
- Download [CSharp Heists v1.1](https://github.com/ste163/csharp_heists/releases) for Windows, Linux, or Mac

### Windows
- Unzip file
- Run ```heist.exe``` to play

### Linux
- Unzip file
- With the terminal, ```cd``` into unzipped directory
- Change permissions on ```CSharpHeists``` file with the following command: ```chmod 777 ./CSharpHeists```
- Still in the terminal, run the following command to play: ```./CSharpHeists```

### Mac (OS X)
>You will get warnings about the safety of this file
- Unzip file, which will create a new directory.
- Open the terminal (from ```Finder```, go to the top of the screen and click ```Go``` then ```Utilities``` then ```Terminal``` )
- ```cd``` into unzipped directory
- Change permissions on ```CSharpHeists``` file with the following command: ```chmod +x CSharpHeists```
- In ```Finder```, enter the directory with the ```CSharpHeists``` file
- Click ```CSharpHeists``` to play (maximize your Terminal window for best results)

## Credits
### Testing
- [Ember Parr](https://www.linkedin.com/in/emberparr/) - for  OS X 64 version
### ASCII Art
Thanks to these sites for the amazing ASCII artwork:
- https://textfancy.com/multiline-text-art/
- https://asciiart.website/
- https://asciiart.website/index.php?art=people/faces
- https://simpletextart.blogspot.com/2013/12/money-bag-picture-made-with-ascii-text.html
- https://www.asciiart.eu/buildings-and-places/cities
- http://www.asciiworld.com/-Buildings-.html

## License
[MIT](/LICENSE)