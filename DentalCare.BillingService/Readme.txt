Install Windows Service:

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe" "C:\Users\fhuang\source\repos\DentalCare.BillingService\bin\Release\DentalCare.BillingService.exe"

Uninstall Windows Service:

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe" /u "C:\Users\fhuang\source\repos\DentalCare.BillingService\bin\Release\DentalCare.BillingService.exe"

Windows service should be run under proper windows account with permission.

Autofac framework is used to implement dependency injection.

Log4net framework is used for logging