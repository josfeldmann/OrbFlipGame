using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsWindow : MonoBehaviour
{
    public bool open = false;
    public bool openOnAwake = false;
    public UnityEvent onCloseEvent;

    public SimpleOptionDisplayerWindow simpleOptionDisplayer;

    private void Awake() {
        if (gameObject.activeInHierarchy) open = true;
        else open = false;
        if (openOnAwake) Open();
    }

    public void Open() {
        open = true;
        gameObject.SetActive(true);
        simpleOptionDisplayer.gameObject.SetActive(false);
    }

    public void OpenToggle() {
        open = !open;

        if (open) {
            Open();
        } else {
            Close();
        }

    }


    public void Close() {
        open = false;
        gameObject.SetActive(false);
        Options.SaveOptions();
        onCloseEvent.Invoke();
    }

    public void ClearSaveData() {
        Debug.Break();
    }

    public void DeleteButton() {
        StopAllCoroutines();
        StartCoroutine(DeleteProgress());
    }

    public IEnumerator DeleteProgress() {
        simpleOptionDisplayer.gameObject.SetActive(true);
        simpleOptionDisplayer.SetOptions("Delete all progress", new List<string>() { "Yes", "No" });

        while (!simpleOptionDisplayer.isDone) {
            yield return null;
        }

        if (simpleOptionDisplayer.result == 0) {
            Options.SetInt(Options.ORBSFLIPPPED, 0);
            Options.SetInt(Options.TWOSFLIPPPED, 0);
            Options.SetInt(Options.THREESFLIPPPED, 0);
            Options.SetInt(Options.HIGHSCORE, 0);
            Options.SetInt(Options.TOTALCOINS, 0);

        } else {

        }
        simpleOptionDisplayer.gameObject.SetActive(false);
    }

}
