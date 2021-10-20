using EshopPOC.API;
using UnityEngine;
using UnityEngine.UI;

namespace EshopPOC
{
    [RequireComponent(typeof(Text))]
    public class SetTextToUserEmail : MonoBehaviour
    {
        private Text _text;

        private void Awake() => _text = GetComponent<Text>();

        private void OnEnable() => UserManager.OnUserChange += HandleUserChange;

        private void OnDisable() => UserManager.OnUserChange -= HandleUserChange;

        private void HandleUserChange(User user)
        {
            _text.text = user.email;
        }
    }
}