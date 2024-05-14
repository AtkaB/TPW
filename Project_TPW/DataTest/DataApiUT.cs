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
            double prev_x = api.GetX(0);
            double prev_y = api.GetX(0);
            api.BallEvent += (sender, args) =>
            {
                Assert.AreNotEqual(prev_x, api.GetX(0));
                Assert.AreNotEqual(prev_y, api.GetY(0));
            };
        }
    }
}