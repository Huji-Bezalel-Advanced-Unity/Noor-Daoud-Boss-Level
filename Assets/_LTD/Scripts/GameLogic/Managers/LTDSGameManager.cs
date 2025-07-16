using LTD.Core.Enemies;
using LTD.Core.Player;
using UnityEngine;

namespace LTD.Core.Managers.AudioManager
{
    public class LTDGameManager
    {
        public LTDPlayer _playerPrefab;
        private Vector3 _spawnPosition = new Vector3(1f, -11f, 0.244f);
        public LTDPlayer Player { get; private set; }
        
        public LTDBoss _bossPrefab;
        private Vector3 _bossSpawnPosition = new Vector3(2, 0f, 0.244F);
        public LTDBoss Boss { get; private set; }

        
        
        public LTDGameManager()
        {
            _playerPrefab = Resources.Load<LTDPlayer>("PlayerPrefab");
            _bossPrefab = Resources.Load<LTDBoss>("BossPrefab");
        }

        public void InstantiatePlayer()
        {
            Player = UnityEngine.Object.Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity);
        }   
     
        public void InstantiateBoss()
        {
            Boss = UnityEngine.Object.Instantiate(_bossPrefab);
            Boss.transform.position = _bossSpawnPosition;
        }
    }
}