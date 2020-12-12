# C# Heists
>Text-based heist adventure game for the terminal

![C# Heists overview GIF](/readme_overview.gif)

## Features
- manage a crew of untrustworthy criminals
    - hire associates
    - ice unruly associates
    - manage morale (unhappy associates may turn against you!)
    - each associate has a unique ASCII face
- perform five heists throughout Nashville
    - each with ASCII art
- six endings based on how much money you got away with, whether you survived, or got arrested
- enough randomization so no two playthroughs have the same outcome

## Known Issues
- Pressing ```enter``` multiple times during a heist countdown will skip heist summary
- Text distortion if terminal window is not wide enough. Maximizing window fixes this
- Background color may not resize correctly on Linux. Restarting game with a maximized window fixes this

## Setup
- Download [CSharp Heists v1.1]() for Windows, Linux, or Mac

### Windows
- Unzip file
- Run ```heist.exe``` to play

### Linux
- Unzip file
- With the terminal, ```cd``` into unzipped directory
- Change permissions on ```CSharpHeists``` file with the following command: ```chmod 777 ./CSharpHeists```
- Still in the terminal, run the following command to play: ```./CSharpHeists```

### Mac (needs testing)
- Unzip file (you will get a warning about this file possibly being unsafe)
- With the terminal, ```cd``` into unzipped directory
- Change permissions on ```CSharpHeists``` file with the following command: ```chmod +x CSharpHeists```
- Still in the terminal, run the following command to play: ```./CSharpHeists```

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