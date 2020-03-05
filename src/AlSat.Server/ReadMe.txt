- To test with Postman, SSL is disabled on Debug options.
	Go to project "settings -> Debug" tab and look for "Enable SSL"

or 

	Disable SSL certificate verification in Postman.
	Go to "File -> Settings"

// To create migrations, use below shell commands.
add-migration <name> -c MainDbContext <!!-o MigrationsMain>
add-migration <name> -c LogDbContext <!!-o MigrationsLog>

// Samples
add-migration InitialCreate -c MainDbContext -o MigrationsMain
add-migration InitialCreate -c LogDbContext -o MigrationsLog

// To update databases, use below shell commands.
update-database -c MainDbContext
update-database -c LogDbContext

// NLog
To enable internal logging to find logging errors, modify nlog.config.
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	autoReload="true"
	internalLogLevel="Trace"
	internalLogFile="c:\Temp\Nlog.txt"
	throwExceptions="true">

