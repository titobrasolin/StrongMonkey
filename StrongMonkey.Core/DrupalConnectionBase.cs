using System;
using System.IO;
using NLog;

namespace StrongMonkey.Core
{
	public abstract class DrupalConnectionBase
	{
		Logger log = LogManager.GetCurrentClassLogger ();
		public delegate void UpdateLog (string str, string mSender, LogLevel mType, bool verbose);

		public event UpdateLog OnUpdateLog;
		
		public void SendLogEvent (string message, string mSender, LogLevel mType)
		{
			SendLogEvent (message, mSender, mType, false);
		}

		private void SendLogEvent (string message, string mSender, LogLevel mType, bool verbose)
		{
			LogEventInfo logEvent = new LogEventInfo(mType, mSender, message);
			logEvent.Properties.Add("verbose", verbose);
			log.Log(logEvent);
			
			if (OnUpdateLog != null) {
				OnUpdateLog (message, mSender, LogLevel.Error, false);
			}
		}

		protected void SendErrorLogEvent (string message, string mSender)
		{
			SendLogEvent(message, mSender, LogLevel.Error);
		}
	}
}

