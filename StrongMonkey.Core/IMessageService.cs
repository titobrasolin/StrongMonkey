using System;


namespace StrongMonkey.Core
{
	public interface IMessageService : IService
	{
		int ShowError (string message);
		int ShowError (string message, bool wrap);

		int ShowWarning (string message);
		int ShowWarning (string message, bool wrap);
		int ShowWarning (string message, bool wrap, string reject_string, string accept_string);

		int ShowWarningWithYesNo (string message);
		int ShowWarningWithYesNo (string message, bool wrap);

		int ShowWarningWithOkCancel (string message);
		int ShowWarningWithOkCancel (string message, bool wrap);

		int ShowInfo (string message);
		int ShowInfo (string message, bool wrap);

		int ShowQuestion (string message);
		int ShowQuestion (string message, bool wrap);
		int ShowQuestion (string message, bool wrap, string reject_string, string accept_string);
	}
}
