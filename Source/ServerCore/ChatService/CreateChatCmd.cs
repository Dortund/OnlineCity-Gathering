﻿using OCUnion;
using ServerOnlineCity.Model;
using ServerOnlineCity.Services;
using System;
using Model;
using System.Collections.Generic;
using Transfer;
using OCUnion.Transfer.Types;

namespace ServerOnlineCity.ChatService
{
    internal sealed class CreateChatCmd : IChatCmd
    {
        public string CmdID => "createchat";

        public Grants GrantsForRun => Grants.UsualUser;

        public string Help => ChatManager.prefix + "createchat :Create private chat";

        public ModelStatus Execute(ref PlayerServer player, Chat chat, List<string> argsM)
        {
            var myLogin = player.Public.Login;
            if (argsM.Count < 1)
                return ChatManager.PostCommandPrivatPostActivChat(ChatCmdResult.SetNameChannel, myLogin, chat, "No new channel name specified");

            var nChat = new Chat()
            {
                Name = argsM[0],
                OwnerLogin = myLogin,
                OwnerMaker = true,
                PartyLogin = new List<string>() { myLogin, "system" },
                Id = Repository.GetData.GetChatId(),
            };

            nChat.Posts.Add(new ChatPost()
            {
                Time = DateTime.UtcNow,
                Message = "User " + myLogin + " created a channel " + argsM[0],
                OwnerLogin = "system"
            });

            player.Chats.Add(nChat);

            if (argsM.Count > 1)
            {
                ChatManager.PostCommandAddPlayer(player, nChat, argsM[1]);
            }

            Repository.Get.ChangeData = true;

            return new ModelStatus();
        }
    }
}
