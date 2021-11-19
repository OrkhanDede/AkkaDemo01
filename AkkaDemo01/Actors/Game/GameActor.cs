using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaDemo01.Actors.Game.Messages;
using AkkaDemo01.Actors.Game.Model;

namespace AkkaDemo01.Actors.Game
{
    public class GameActor : ReceiveActor
    {
        private readonly Guid _id;
        private bool _gameHasStarted;
        public BoardState BoardState { get; set; } = new BoardState();

        public GameActor(Guid id)
        {
            _id = id;
            Receive<StartGame>(StartGameHandler);
            Receive<PutBone>(PutBoneHandler);
        }

        private void PutBoneHandler(PutBone message)
        {
            if (!_gameHasStarted)
            {
                Console.WriteLine($"Error: game was not started yet");
            }
            else
            {
                BoardState.PutLeft(new Bone()
                {
                    Left = message.Left,
                    Right = message.Right
                });
                
                Console.WriteLine($"Bone {message.Left}|{message.Right} was put");
            }
        }

        private void StartGameHandler(StartGame message)
        {
            _gameHasStarted = true;
            Console.WriteLine($"GameActor {_id} has started");
        }

        public static Props Props(Guid id)
        {
            return Akka.Actor.Props.Create(() => new GameActor(id));
        }
    }
}
