using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;
using System;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class _3社で忘年会を開く
    {
        private const string MEETING_ID = "1";

        private ServerDriver _server;
        private ClientDriver _client;

        [TestInitialize]
        public void Setup()
        {
            _server = new ServerDriver();
            _client = new ClientDriver(_server.CreateClient());
        }

        [TestMethod]
        public void 忘年会を作成して実施する()
        {
            忘年会を作成する();
            忘年会が作成されたことを確認する();
            忘年会にスポンサーを追加する();
            忘年会にスポンサーが追加される();
            招待者を6人追加する();
            招待者が返信なしで追加されたことを確認する();
            招待者が不参加の返信をする();
            不参加の返信をした招待者が不参加になっていることを確認する();
            招待者が参加の返信をする();
            参加の返信をした招待者が参加者になっていることを確認する();
            残りの招待者が参加の返信をする();
            残りの招待者が参加者になっていることを確認する();
            飛び込みの参加者を登録する();
            飛び込みの参加者が登録されたことを確認する();
            //参加者が参加をキャンセルする();
            //参加者が参加をキャンセルされたことを確認する();
            //忘年会を開催する();
            //忘年会が開催になったことを確認する();
            参加者が忘年会に出席する();
            参加者が忘年会に出席したことを確認する();
            残りの参加者が忘年会に出席する();
            残りの参加者が忘年会に出席したことを確認する();
            忘年会の費用を登録する();
            参加者全員分忘年会の費用が計算されていることを確認する();
        }

        private void 忘年会を作成する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会が作成されたことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会にスポンサーを追加する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会にスポンサーが追加される()
        {
            throw new NotImplementedException();
        }

        private void 招待者を6人追加する()
        {
            throw new NotImplementedException();
        }

        private void 招待者が返信なしで追加されたことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 招待者が不参加の返信をする()
        {
            throw new NotImplementedException();
        }

        private void 不参加の返信をした招待者が不参加になっていることを確認する()
        {
            throw new NotImplementedException();
        }

        private void 招待者が参加の返信をする()
        {
            throw new NotImplementedException();
        }

        private void 参加の返信をした招待者が参加者になっていることを確認する()
        {
            throw new NotImplementedException();
        }

        private void 残りの招待者が参加の返信をする()
        {
            throw new NotImplementedException();
        }

        private void 残りの招待者が参加者になっていることを確認する()
        {
            throw new NotImplementedException();
        }

        private void 飛び込みの参加者を登録する()
        {
            throw new NotImplementedException();
        }

        private void 飛び込みの参加者が登録されたことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 参加者が参加をキャンセルする()
        {
            throw new NotImplementedException();
        }

        private void 参加者が参加をキャンセルされたことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会を開催する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会が開催になったことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 参加者が忘年会に出席する()
        {
            throw new NotImplementedException();
        }

        private void 参加者が忘年会に出席したことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 残りの参加者が忘年会に出席する()
        {
            throw new NotImplementedException();
        }

        private void 残りの参加者が忘年会に出席したことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会の費用を登録する()
        {
            throw new NotImplementedException();
        }

        private void 参加者全員分忘年会の費用が計算されていることを確認する()
        {
            throw new NotImplementedException();
        }
    }
}
