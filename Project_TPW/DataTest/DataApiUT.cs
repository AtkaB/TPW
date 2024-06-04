using Data;

namespace DataTest
{
    [TestClass]
    public class DataApiUT
    {
        DataApi api = DataApi.Instance();
        [TestMethod]
        public void CreateBallsTest()
        {
            Assert.IsNotNull(api);
            api.CreateBalls(3);
            Assert.AreEqual(3, api.GetNumberOfBalls());
        }

        [TestMethod]
        public void MoveTest()
        {
            Assert.IsNotNull(api);
            api.CreateBalls(1);
            Assert.AreEqual(1, api.GetNumberOfBalls());
            double prev_x = api.GetPosition(0).X;
            double prev_y = api.GetPosition(0).Y;
            api.BallEvent += (sender, args) =>
            {
                Assert.AreNotEqual(prev_x, api.GetPosition(0).X);
                Assert.AreNotEqual(prev_y, api.GetPosition(0).X);
            };
        }
    }
}