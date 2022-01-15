using UnityEngine;
using UnityEngine.UI;

public class GameMasterManager : MonoBehaviour
{
    public StateMachine<GameMasterManager> controller;

    public GameObject mainMenu, tutorial;
    public OptionsWindow optionsWindow;
    public CanvasScaler canvasScaler;
    public StatScreen statScreen;
    public bool doMobileScaling;
    public FlipController flipController;
    
    private void Awake() {
        controller = new StateMachine<GameMasterManager>(new MainMenuState(), this);

        if (!Options.HasInt(Options.TOTALCOINS)) {
            Options.SetInt(Options.TOTALCOINS, 0);
        }

        if (!Options.HasInt(Options.THREESFLIPPPED)) {
            Options.SetInt(Options.THREESFLIPPPED, 0);
        }

        if (!Options.HasInt(Options.TWOSFLIPPPED)) {
            Options.SetInt(Options.TWOSFLIPPPED, 0);
        }

        if (!Options.HasInt(Options.ORBSFLIPPPED)) {
            Options.SetInt(Options.ORBSFLIPPPED, 0);
        }


        if (!Options.HasInt(Options.HIGHSCORE)) {
            Options.SetInt(Options.HIGHSCORE, 0);
        }

        if (doMobileScaling) {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
                canvasScaler.matchWidthOrHeight = 1;
            } else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.WindowsEditor) {

            } else {
                canvasScaler.matchWidthOrHeight = 0;
            }
        }

    }

    public void HideAll() {
        tutorial.SetActive(false);
        mainMenu.SetActive(false);
        optionsWindow.gameObject.SetActive(false);
        flipController.gameObject.SetActive(false);
        statScreen.gameObject.SetActive(false);
    }

    public void GoToMainMenu() {
        controller.ChangeState(new MainMenuState());
    }

    public void GoToOptionsMenu() {
        controller.ChangeState(new OptionsState());
    }

    public void GoToGameplayMenu() {
        controller.ChangeState(new GameplayState());
    }


    public void GoToStatsMenu() {
        controller.ChangeState(new StatsState());
    }

    public void GoToTutorial() {
        controller.ChangeState(new TutorialState());
    }

    public void Quit() {
        Application.Quit();
    }



}


public class MainMenuState : State<GameMasterManager> {
    public override void Enter(StateMachine<GameMasterManager> obj) {
        obj.target.HideAll();
        obj.target.mainMenu.SetActive(true);
    }
}
public class OptionsState : State<GameMasterManager> {
    public override void Enter(StateMachine<GameMasterManager> obj) {
        obj.target.HideAll();
        obj.target.optionsWindow.Open();
    }
}
public class GameplayState : State<GameMasterManager> {
    public override void Enter(StateMachine<GameMasterManager> obj) {
        obj.target.HideAll();
        obj.target.flipController.gameObject.SetActive(true);
        obj.target.flipController.Setup();
    }
}

public class StatsState : State<GameMasterManager> {
    public override void Enter(StateMachine<GameMasterManager> obj) {
        obj.target.HideAll();
        obj.target.statScreen.gameObject.SetActive(true);
        obj.target.statScreen.SetUp();
    }
}

public class TutorialState : State<GameMasterManager> {
    public override void Enter(StateMachine<GameMasterManager> obj) {
        obj.target.HideAll();
        obj.target.tutorial.gameObject.SetActive(true);
        
    }
}


