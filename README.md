# SmartChargingAssignment (Version: 19)
# Camilo Avila

#1
-Clone the repository and open up the solution inside SmartCharging folder. 
-If you have downloaded de zip file just open up the solution in SmartCharging folder.

#2
Create a new local server in the "SQL Server Object Explorer"

![image](https://github.com/Juan-Avila92/SmartChargingAssignment/assets/43795308/3e0b4600-f295-49bd-93e4-509cdbf2dc14)

Inside your program.cs file in "SmartChargin.API" folder you should 

#3
Ensure that your "SmartChargin.API" folder has got the follow dependencies installed:
Frameworks: Microsof.AspNetCore.App - Microsoft.NETCore.App - All of them are version 8.0.2
Packages: Microsoft.EntityFrameworkCore.Design - Microsoft.EntityFrameworkCore.SqlServer - Microsoft.EntityFrameworkCore.Tools - All of them are version 8.0.2

![image](https://github.com/Juan-Avila92/SmartChargingAssignment/assets/43795308/f716a376-7a8e-47df-81ea-f5b5b148d849)

#4
Ensure that yout "Tests" folder has got the follow dependencies installed:
Frameworks: Microsof.AspNetCore.App - Microsoft.NETCore.App - All of them are version 8.0.2
Packages: Microsoft.NET.Test.Sdk (17.8.0) - NSubstitute (5.1.0) - NUnit (3.14.0) - NUnitAnalyzers (3.9.0) - NUnit3TestAdapter (4.5.0)

![image](https://github.com/Juan-Avila92/SmartChargingAssignment/assets/43795308/15295f45-30f2-454a-85b0-a14fbe85cc68)

#5 (Important)
Please use the following body to call Post method in ChargeStation API

{
  "name": "string",
  "connectors": [
 {
      "maxCurrentInAmps": 1
    }
  ]
}

