# Inventory-Sorter

![image](https://user-images.githubusercontent.com/46509577/236335631-9830a551-c713-4f73-bb77-50a2ea347d35.png)


## Information
For Game Version 1.2.03  
For PML Version 0.11.1  
Mod Version 1.1.0  
Developed by: Dragon  
Host/Client Requirements: Client | Host (sorting affects joining clients)

Support the developer: https://www.patreon.com/DragonFire47


## Installation 
- have PulsarModLoader installed  
- go to \PULSARLostColony\Mods  
- add the .dll included with this package

## Features
- Adds multiple sorting modes and control via commands + GUI
- Adds customizable sort mode, controllable via GUI (F5 menu).


## Commands:
/sortby [sortmode] | [Fix]  
Sortmodes: Type, Name, Default, Custom. Mode can be shortened to their initial ie: Type = t  
ex: /sortby t

/sortby fix - Fixes issues with level/subtype sorting. Use this if items aren't getting their level sorted correctly. Consider making a backup save first.

Known Issues:
- Sometimes item levels don't get sorted properly. This is caused by subtype sorting (needed for food) and silly game devs. A workaround has been implemented with /sortby fix.
