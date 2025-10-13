using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    internal class User : Singleton<User>
    {
        private SkillBridge.Message.NUserInfo userInfo;

        public SkillBridge.Message.NUserInfo Info
        {
            get { return userInfo; }
        }

        public void SetupUserInfo(SkillBridge.Message.NUserInfo info)
        {
            this.userInfo = info;
        }

        public SkillBridge.Message.NCharacterInfo CurrentCharacter { get; set; }
    }
}