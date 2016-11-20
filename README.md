# EnergySaveService
Windows Service to send Computer in Energy Saving Mode

This little Windows Service executable sends Windows directly to Energy saving Mode when started.
I use it to turn my PC off via an Samba RPC-Call.

Install:

1. Download the .zip-File

2. Unpack it to a local Directory

3. Run "installService.cmd" to install the Service


Remove:
You can remove the Service with the "removeService.cmd".

After that delete all Files and you are done.

Optional:
If you want to run a Service via RPC to have to import the "LocalAccountTokenFilterPolicy.reg" by double clicking it.

What does the executable?

The Service calls the "powrprof.dll" and runs "SetSuspendState", it also writes an entry to the Windows Logs.
