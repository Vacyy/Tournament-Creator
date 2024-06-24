# Tournament Creator

Tournament Creator is a simple C# Windows Forms application that allows users to create and manage tournament brackets easily.

## System Requirements

- Windows 10 or newer
- .NET Framework 6.0 or newer
- XAMPP Control Panel

## Viewing Code

   To view all the .cs and .Designer.cs files go to /Code/ folder.
   
## Installation

1. Launch XAMPP Control Panel and start Apache and MySQL modules.
2. Create and set up the database:
   - Create a new database named `tournamentdb`.
   - Import `tournamentdb.sql` into the database.
   - Ensure the user `Tester` exists and has the necessary permissions:
     ```sql
     CREATE USER 'Tester'@'localhost' IDENTIFIED BY '123';
     GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON *.* TO 'Tester'@'localhost';
     FLUSH PRIVILEGES;
     ```
3. To run program
   - Download `Tournament Creator.zip`
   - unzip the folder
   - run `TournamentCreatorWinForms.exe` from `Tournament Creator`
  

     
## Usage

1. **First Launch:**
   - Register by entering your details.
   - Log in using the registered details.
   - To login on admin panel use:
       username: admin
       password: admin

2. **After Login:**
   - Create tournaments, manage teams, and save tournament data.
   - Browse and view created tournaments.
   - Mark match winners and confirm standings.

3. **Admin Interface:**
   - Manage users and tournaments.

## Architecture

- **Program Files:**
  - `.cs` files for application logic.
  - `Designer.cs` files for UI design.

- **Database:**
  - All sql commands are in `tournamentdb.sql`
  - DB quickly access Procedures go to `/Code/Procedures.txt`.
 
## Authors
- Mikołaj Pałka (me)
- Kamil Orliński (https://github.com/KamilOrlinski)
- Kamil Paluszak (https://github.com/Kamiluniuniu)
- Jagoda Różycka


