using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace DoorLockCoreTest
{
    [TestFixture]
    public class DoorLockTests
    {
        [Test]
        public void DoorLockCtorValidCodesInitialized()
        {
            //Arrange
            //Act
            DoorLock Lock = new DoorLock();
            //Assert
            Assert.That(Lock.SubmitCode("000000000"), Is.True);
            Assert.That(Lock.SubmitCode("987654321"), Is.True);
            Assert.That(Lock.SubmitCode("123456"), Is.True);            
        }
        [Test]
        public void DoorLockStartsUnlocked()
        {
            //Arrange
            //Act
            DoorLock Lock = new DoorLock();
            //Assert
            Assert.That(Lock.IsLocked, Is.False);
        }
        [Test]
        public void DoorLockSumbitCodeTogglesState()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            Assert.That(Lock.IsLocked, Is.False);
            Lock.SubmitCode("000000000");            
            //Assert
            Assert.That(Lock.IsLocked, Is.True);
        }
        [Test]
        public void DoorLockCtorAttmeptsAllowed()
        {
            //Arrange
            //Act
            DoorLock Lock = new DoorLock();            
            //Assert
            Assert.That(Lock.AttemptsAllowed, Is.EqualTo(3));
        }
        [Test]
        public void DoorLockCtorAttmeptsNumber()
        {
            //Arrange
            //Act
            DoorLock Lock = new DoorLock();
            //Assert
            Assert.That(Lock.AttemptNumber, Is.EqualTo(1));
        }
        [Test]
        public void DoorLockSubmitCodeIncrementsAttempts()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            Lock.SubmitCode("44344");
            //Assert
            Assert.That(Lock.AttemptNumber, Is.EqualTo(2));
        }
        [Test]
        public void DoorLockLockOutInterval()
        {
            //Arrange
            //Act
            DoorLock Lock = new DoorLock();            
            //Assert
            Assert.That(Lock.LockOutTime, Is.EqualTo(10000));
        }
        [Test]
        public void DoorLockOnlyLetsValidCodes()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            //Assert
            Assert.That(Lock.SubmitCode(Lock.AttemptsAllowed.ToString()), Is.False);
            Assert.That(Lock.SubmitCode(Lock.LockOutTime.ToString()), Is.False);

        }
        [Test]
        public void DoorLockDisabled()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            //Assert
            Assert.That(Lock.IsDisabled, Is.False);
        }
        [Test]
        public void DoorLockSumbitCodeReturnsBool()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            
            //Assert
            Assert.That(Lock.SubmitCode("1221212121212121"),Is.False);
        }
        [Test]
        public void DoorLockDisabledAfterAttempts()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            Lock.SubmitCode("44344");
            Assert.That(Lock.IsDisabled, Is.False);
            Lock.SubmitCode("44344");
            Assert.That(Lock.IsDisabled, Is.False);
            Lock.SubmitCode("44344");
            //Assert
            Assert.That(Lock.IsDisabled, Is.True);
        }
        [Test]
        public void DoorLockDisabledResetOnValidSubmit()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            Lock.SubmitCode("44344");
            Lock.SubmitCode("44344");
            Lock.SubmitCode("44344");
            Assert.That(Lock.IsDisabled, Is.True);
            Lock.SubmitCode("000000000");
            //Assert
            Assert.That(Lock.IsDisabled, Is.False);
        }

        [Test]
        public void DoorLockLogToString()
        {
            //Arrange
            DoorLock Lock = new DoorLock();
            //Act
            Lock.SubmitCode("000000000");
            //Assert
            Assert.That(Lock.Log.ToString(), Does.EndWith("000000000,Success,Enabled\r\n"));
            Assert.That(DateTime.Parse(Lock.Log.ToString().Split(',')[0]), Is.TypeOf(DateTime.Now.GetType()));
        }
        
    }

}
