using MyUtilities.GUI;
using UnityEngine;

public class ExampleMenu : MonoBehaviour
{
    [SerializeField] private NavigationController navController = default;
    [SerializeField] private ExampleViewController viewController = default;

    public void Btn_OpenExampleMenu()
    {
        navController.Push(viewController);
    }

    public void Btn_CloseExampleMenu()
    {
        navController.Pop();
    }
}
