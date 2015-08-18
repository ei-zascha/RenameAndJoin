using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace RenameAndJoin
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
            // Get and display current machine name
			textBoxOldPCName.Text = Environment.MachineName;
            // Get and display wired NIC's IP address
			textBoxIPAddr.Text = GetIP(GetNIC());
		}

		public void buttonDomain_Click(object sender, EventArgs e)
		{
			string newhostname = "";
			const string hostnamePattern = "^ABC\\d{5}$";

			if (textBoxIPAddr.Text != "")
			{
				string strOU = SetOU(textBoxIPAddr.Text);

				if (checkBoxRenamePC.Checked && checkBoxAdmin.Checked == false)
				{
					newhostname = textBoxNewPCName.Text.ToUpper();
					if (Regex.IsMatch(newhostname, hostnamePattern))
					{
						HandleRenameandJoinDomain("contoso.com", strOU, newhostname, "contoso\\adjoin", "Passw0rd!");
					}
					else
					{
						MessageBox.Show("You MUST enter the PC name as ABC followed by 5 numbers!  For example: \"ABC51931\" ", "New PC Name Incorrect!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				else if (checkBoxRenamePC.Checked && checkBoxAdmin.Checked)
				{
					newhostname = textBoxNewPCName.Text.ToUpper();
					if (!Regex.IsMatch(newhostname, hostnamePattern))
					{
						MessageBox.Show("You MUST enter the PC name as ABC followed by 5 numbers!  For example: \"ABC51931\" ",
							"New PC Name Incorrect!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else if (textBoxDomainUser.Text == "" || textBoxDomainPassword.Text == "")
					{
						MessageBox.Show("Enter valid credentials before continuing!", "Credentials Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						HandleRenameandJoinDomain("contoso.com", strOU, newhostname, "contoso\\" + textBoxDomainUser.Text, textBoxDomainPassword.Text);
					}
				}
				else if (checkBoxAdmin.Checked && checkBoxRenamePC.Checked == false)
				{
					if (textBoxDomainUser.Text == "" || textBoxDomainPassword.Text == "")
					{
						MessageBox.Show("Enter valid credentials before continuing!", "Credentials Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						HandleRenameandJoinDomain("contoso.com", strOU, newhostname, "contoso\\" + textBoxDomainUser.Text, textBoxDomainPassword.Text);
					}
				}
				else
				{
					HandleRenameandJoinDomain("contoso.com", strOU, newhostname, "contoso\\adjoin", "Passw0rd!");
				}
			}
			else
			{
				textBoxOutput.AppendText("System MUST be connected to the wired ABC network before continuing!" +
										 Environment.NewLine);
			}
		}

        // Get the MAC Address from the enabled wired NIC via WMI query
        public string GetNIC()
		{
			string strRet = "";

			try
			{
				var objMOS = new ManagementObjectSearcher("root\\CIMV2",
					"SELECT * FROM Win32_NetworkAdapter Where NetEnabled='True'");
				ManagementObjectCollection objMOC = objMOS.Get();
				if (objMOC.Count > 0)
				{
					foreach (ManagementObject objMO in objMOC)
					{
						strRet = objMO["MACAddress"].ToString();
					}
				}
			}
			catch (ManagementException e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}
			catch (Exception e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}
			return strRet;
		}

        // Get the IP address assigned to the MAC address returned in GetNIC() via WMI query
        public string GetIP(string macAddr)
		{
			string strRet = "";
			ManagementObjectCollection objMOC;

			try
			{
				var objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
				objMOC = objMC.GetInstances();
			}
			catch (ManagementException e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}
			catch (Exception e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}

			// Grab the IP address of the correct adapter
			foreach (ManagementObject objMO in objMOC)
			{
				if (null != objMO)
				{
					if ((string) objMO["MACAddress"] == macAddr)
					{
						if (null != objMO["IPAddress"])
						{
							strRet = ((string[]) objMO["IPAddress"]).FirstOrDefault();
							break;
						}
					}
				}
			}

			return strRet;
		}

        // Get the computer name via WMI query
        public string GetComputerName()
		{
			string strRet = "";
			ManagementObjectCollection objMOC;

			try
			{
				var objMC = new ManagementClass("Win32_ComputerSystem");
				objMOC = objMC.GetInstances();
			}
			catch (ManagementException e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}
			catch (Exception e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}

			foreach (ManagementObject objMO in objMOC)
			{
				if (null != objMO)
				{
					strRet = objMO["Name"].ToString();
					break;
				}
			}

			return strRet;
		}

        // Check if already joined to a domain or not
        public string GetDomainName()
		{
			string strRet = "";
			ManagementObjectCollection objMOC;

			try
			{
				var objMC = new ManagementClass("Win32_ComputerSystem");
				objMOC = objMC.GetInstances();
			}
			catch (ManagementException e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}
			catch (Exception e)
			{
				textBoxOutput.AppendText(e.Message);
				return strRet;
			}

			foreach (ManagementObject objMO in objMOC)
			{
				if (null != objMO)
				{
					if ((bool) objMO["partofdomain"])
					{
						strRet = objMO["domain"].ToString();
						textBoxOutput.AppendText("The computer is currently in the domain: " + strRet + Environment.NewLine);
					}
					else
					{
						textBoxOutput.AppendText("The computer is currently in a Workgroup, not domain." + Environment.NewLine);
					}
				}
			}

			return strRet;
		}

        // Set the OU based on the subnet
        public string SetOU(string strIP)
		{
			string strRet;
			int iSubnet = GetSubnet(strIP);

			switch (iSubnet)
			{
				case 11:
					strRet = "OU=VLAN1-Location1,OU=Computers,OU=Production,OU=Resources,DC=contoso,DC=com";
					break;
				case 13:
					strRet = "OU=VLAN2-Location2,OU=Computers,OU=Production,OU=Resources,DC=contoso,DC=com";
					break;
				case 14:
					strRet = "OU=VLAN3-Location3,OU=Computers,OU=Production,OU=Resources,DC=contoso,DC=com";
					break;
				case 15:
					strRet = "OU=VLAN4-Location4,OU=Computers,OU=Production,OU=Resources,DC=contoso,DC=com";
					break;
				case 16:
					strRet = "OU=VLAN5-Location5,OU=Computers,OU=Production,OU=Resources,DC=contoso,DC=com";
					break;
				default:
					strRet = "OU=Computers,OU=Production,OU=Resources,DC=contoso,DC=com";
					break;
			}

			return strRet;
		}

        // Get the subnet from the IP address
		public int GetSubnet(string strIP)
		{
			int iRet = 0;
			const string strPattern = @"\A10\.200\.([01]?\d\d?|2[0-4]\d|25[0-5])\.[01]?\d\d?|2[0-4]\d|25[0-5](\1)";

			var rgxIPAddr = new Regex(strPattern);
			var rgxSubnet = rgxIPAddr.Match(strIP);

			if (!rgxSubnet.Success) return iRet;
			try
			{
				iRet = Int32.Parse(rgxSubnet.Groups[1].Value);
			}
			catch (FormatException e)
			{
				textBoxOutput.AppendText(e.Message + Environment.NewLine);
			}

			return iRet;
		}
		
        // Attempt to rename the PC and/or join it to the domain
		public void HandleRenameandJoinDomain(string strDomain, string strOU, string strNewHostname, string strUsername, string strPassword)
		{
			string strCurrentHostname = GetComputerName();
			string strCurrent = GetDomainName();

			var objMO = new ManagementObject("Win32_ComputerSystem.Name='" + Environment.MachineName + "'");

			ManagementBaseObject result;

			objMO.Scope.Options.EnablePrivileges = true;
			objMO.Scope.Options.Authentication = AuthenticationLevel.PacketPrivacy;
			objMO.Scope.Options.Impersonation = ImpersonationLevel.Impersonate;

			// Rename computer if option to rename was selected
			if (("" != strNewHostname) &&
				(!String.Equals(strCurrentHostname, strNewHostname, StringComparison.CurrentCultureIgnoreCase)))
			{
				try
				{
					textBoxOutput.AppendText("Please wait while computer is renamed!  This may take up to 30 seconds." + Environment.NewLine);
					textBoxOutput.AppendText("Join Domain: Setting the computer name to " + strNewHostname.ToUpper() +
											 Environment.NewLine);
					ManagementBaseObject query2 = objMO.GetMethodParameters("Rename");
					query2["Name"] = strNewHostname;
					query2["Password"] = null;
					query2["UserName"] = null;

					result = objMO.InvokeMethod("Rename", query2, null);

					if (result != null && 0 != (uint) result["ReturnValue"])
					{
						textBoxOutput.AppendText("Can not change the computer name, error code: " + result["ReturnValue"] +
												 Environment.NewLine);
					}

					textBoxOutput.AppendText("Success!" + Environment.NewLine);
				}
				catch (InvalidOperationException e)
				{
					textBoxOutput.AppendText("Join domain failed: " + e.Message + Environment.NewLine);
					return;
				}
				catch (ManagementException e)
				{
					textBoxOutput.AppendText("Join domain failed, could not change the computer name: " + e.Message +
											 Environment.NewLine);
					return;
				}
				// Pause 20 seconds after renaming computer
				Thread.Sleep(20000);
			}

			// Fail if already joined to the contoso domain
			if ("" != strCurrent && strCurrent.ToUpper() == strDomain.ToUpper())
			{
				textBoxOutput.AppendText("This computer is already on the domain as: " + strCurrentHostname + "." + strCurrent +
										 Environment.NewLine);
				return;
			}
			// Fail if already joined to another domain
			if ("" != strCurrent)
			{
				textBoxOutput.AppendText("This computer is already on another domain: " + strCurrent + Environment.NewLine);
				return;
			}

			ManagementBaseObject query;
			try
			{
				query = objMO.GetMethodParameters("JoinDomainOrWorkgroup");
			}
			catch (ManagementException)
			{
				textBoxOutput.AppendText("Join domain failed, could not find the local computer (???)." + Environment.NewLine);
				return;
			}
			query["Name"] = strDomain;
			query["Password"] = strPassword;
			query["UserName"] = strUsername;
			query["AccountOU"] = strOU;
			query["FJoinOptions"] = 0x1 + 0x2 + 0x400;

			textBoxOutput.AppendText("Trying WMI method JoinDomainOrWorkgroup." + Environment.NewLine +
									"Domain: " + query["Name"] + Environment.NewLine +
									"Username: " + query["UserName"] + Environment.NewLine +
									"Container: " + Environment.NewLine +
									query["AccountOU"] + Environment.NewLine +
									"JoinDomainOrWorkgroup flags: " + query["FJoinOptions"] + Environment.NewLine);

			try
			{
				result = objMO.InvokeMethod("JoinDomainOrWorkgroup", query, null);
			}
			catch (ManagementException e)
			{
				textBoxOutput.AppendText("Join domain failed code: " + (uint) e.ErrorCode + " could not execute the query: " +
										 e.Message + Environment.NewLine);
				return;
			}

			if (result != null && 0 != (uint) result["ReturnValue"])
			{
				textBoxOutput.AppendText("Join domain failed: " + (uint) result["ReturnValue"] + " could not execute the query." +
										 Environment.NewLine);
			}

			if (result != null && 0 == (uint) result["ReturnValue"])
			{
				textBoxOutput.AppendText("Domain join succeeded." + Environment.NewLine);
				
				if (MessageBox.Show("Reboot the computer now?", "Reboot PC", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Reboot();
				}
				else
				{
					Activate();
					textBoxOutput.AppendText("You must reboot the computer to continue." + Environment.NewLine);
				}
			}
		}

        // Reboot the PC
		private void Reboot()
		{
			var objMO = new ManagementObject("Win32_OperatingSystem=@");
			objMO.Scope.Options.EnablePrivileges = true;

			ManagementBaseObject result;

			try
			{
				result = objMO.InvokeMethod("Reboot", null, null);
			}
			catch (ManagementException e)
			{
				textBoxOutput.AppendText("Unable to restart the computer:  " + e.Message + Environment.NewLine);
				return;
			}

			if (result != null && 0 != (uint)result["ReturnValue"])
			{
				textBoxOutput.AppendText("Join domain failed: " + (uint)result["ReturnValue"] + " could not execute the query." +
										 Environment.NewLine);
			}

		}

		private void checkBoxRenamePC_CheckedChanged(object sender, EventArgs e)
		{
			textBoxNewPCName.Enabled = checkBoxRenamePC.Checked;
		}

		private void checkBoxAdmin_CheckedChanged(object sender, EventArgs e)
		{
			textBoxDomainUser.Enabled = checkBoxAdmin.Checked;
			textBoxDomainPassword.Enabled = checkBoxAdmin.Checked;
		}

	}
}