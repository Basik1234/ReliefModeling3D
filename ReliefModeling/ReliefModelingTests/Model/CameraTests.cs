using NUnit.Framework;
using OpenTK;
using ReliefModeling.Model.Controls;

namespace ReliefModelingTests
{
    [TestFixture]
    public class CameraTests
    {
        [Test]
        public void Longitude_90DegreesToLeft_CameraMoveRelativeCenter()
        {
            var camera = new Camera(1,90,0);

            camera.Longitude += 90;
            
            Assert.AreEqual(new Vector3(1,0,0),camera.Transform);
        }
        
        [Test]
        public void Longitude_90DegreesToRight_CameraMoveRelativeCenter()
        {
            var camera = new Camera(1,90,0);

            camera.Longitude -= 90;
            
            Assert.AreEqual(new Vector3(-1,0,0),camera.Transform);
        }

        [Test]
        public void PolarDistance_90DegreesToUp_CameraMoveRelativeCenter()
        {
            var camera = new Camera(1,90,0);

            camera.PolarDistance -= 90;
            
            Assert.AreEqual(new Vector3(0,1,0),camera.Transform);
        }
        
        [Test]
        public void PolarDistance_90DegreesToDown_CameraMoveRelativeCenter()
        {
            var camera = new Camera(1,90,0);

            camera.PolarDistance += 90;
            
            Assert.AreEqual(new Vector3(0,-1,0),camera.Transform);
        }
        
        [Test]
        public void Radius_1IncreaseBy4_5Return()
        {
            var camera = new Camera(1,90,0);

            camera.Radius += 4;
            
            Assert.AreEqual(new Vector3(0,0,5),camera.Transform);
        }
        
        [Test]
        public void Radius_5DecreaseBy4_1Return()
        {
            var camera = new Camera(5,90,0);

            camera.Radius -= 4;
            
            Assert.AreEqual(new Vector3(0,0,1),camera.Transform);
        }
        
        [Test]
        public void Radius_1DecreaseBy4_0Return()
        {
            var camera = new Camera(1,90,0);

            camera.Radius -= 4;
            
            Assert.AreEqual(new Vector3(0,0,0), camera.Transform);
        }
        
        [Test]
        public void Up_1TurnOverThroughTop_InvertUpVectorReturn()
        {
            var camera = new Camera(1,90,0);

            camera.PolarDistance -= 180;
            
            Assert.AreEqual(new Vector3(0,-1,0),camera.Up);
        }
        
        [Test]
        public void Up_1TurnOverThroughDown_InvertUpVectorReturn()
        {
            var camera = new Camera(1,90,0);

            camera.PolarDistance += 180;
            
            Assert.AreEqual(new Vector3(0,-1,0),camera.Up);
        }
    }
}