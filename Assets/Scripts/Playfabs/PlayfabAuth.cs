
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class PlayfabAuth : MonoBehaviour
{
    public InputField user;
    public InputField pass;
    public InputField email;
    public Text message;
    public bool isAuthentiacted = false;
    public LoginWithPlayFabRequest loginRequest;
    // Start is called before the first frame update
    void Start()
    {
        email.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        loginRequest = new LoginWithPlayFabRequest();
        loginRequest.Username = user.text;
        loginRequest.Password = pass.text;
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, result =>
        {
            isAuthentiacted = true;
            message.text = "Wellcome " + user.text + " Connecting ...";
            Debug.Log("You are now logged in");
        }, error =>
        {
            message.text = "Loi login [" + error.ErrorMessage + "]";
            isAuthentiacted = false;
            email.gameObject.SetActive(true);
            Debug.Log(error.ErrorMessage);
        },null);
    }

    public void Register()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email = email.text;
        request.Username = user.text;
        request.Password = pass.text;
        PlayFabClientAPI.RegisterPlayFabUser(request, result =>
        {
            message.text = "Tao tk tanh cong";
        }, error =>
        {
            message.text = "Loi tao tk [" + error.ErrorMessage + "]";
        });
    }
}
