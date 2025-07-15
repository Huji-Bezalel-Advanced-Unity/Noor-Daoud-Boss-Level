using LTD.Core.Player;
using UnityEngine;

public class LTDPlayerAnimationRelay : MonoBehaviour
{
    private LTDPlayer _player;

    private void Awake()
    {
        _player = GetComponentInParent<LTDPlayer>();
    }

    
    public void ShootSpell()
    {
        _player.Wand?.Fire();
    }
}