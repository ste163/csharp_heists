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
- Distorted text if terminal window is not wide enough. Maximizing window fixes this.
- Pressing any key during a heist countdown will skip the heist summary.

## Setup
### Windows
- Download ```win64_csharp-heists-v1.0.zip``` from: UPDATED LINK v1.1
- Unzip file
- Run ```heist.exe```

### Linux
- Download ```lin64_csharp-heists-v1.0.zip``` from: UPDATED LINK v1.1
- Unzip file and cd into directory
- Change permissions on CSharpHeists file with ```chmod 777 ./CSharpHeists```
- Run with ```./CSharpHeists```

### Mac
- Download ```version``` from from: UPDATED LINK v1.1
- Unzip file and cd into directory
- CHANGE PERMISSIONS
- Run

### Build application
#### Prerequisites
- .NET 5
#### Download the project
- ```git clone``` repo and ```cd``` into it.
```
git clone git@github.com:ste163/csharp_heists.git
cd csharp_heists
```
#### Build & run the application
- Inside the ```csharp_heists``` directory, run ```dotnet run``` to build and run the application.

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