﻿using MediatR;
using SozlukAppCommon.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukAppCommon.Models.RequestModels
{
    public class LoginUserCommand: IRequest<LoginUserViewModel>
    {
        public string EmailAdress { get; private set; }
        public string Password { get; private set; }

        public LoginUserCommand(string emailAdress, string password)
        {
            EmailAdress = emailAdress;
            Password = password;
        }

        public LoginUserCommand()
        {
                
        }
    }
}
