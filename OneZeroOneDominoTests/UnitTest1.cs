using Akka.Actor;
using Akka.TestKit.Xunit;
using OneZeroOneDomino.Actors.Manager;
using Xunit;

namespace OneZeroOneDominoTests
{
    public class UnitTest1:TestKit
    {
        [Fact]
        public void Test1()
        {
            var subject =
                this.Sys.ActorOf(Props.Create(()=>new GameManagerActor()));

        }
    }
}
