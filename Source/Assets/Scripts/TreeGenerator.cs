using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    [SerializeField] private TMP_InputField input;
    private int[] _numbers;
    


    public void Execute()
    {
        String[] tokens = input.text.Split(' ');
        _numbers=new int[tokens.Length];
        int i = 0;
        foreach (String str in tokens)
        {
            _numbers[i++] = int.Parse(str);
        } 
        //"numbers" array is filled with the raw input.
        
        
        var rootVal = _root.transform.GetChild(0).gameObject;
        if (!rootVal.activeSelf) //Check if the root has a value or not
        {
            _root.GetComponent<NodeController>().SetValue(_numbers[0]);
            rootVal.SetActive(true);
            StartCoroutine("TimedEvent",1);
        }
        else
        {
            StartCoroutine("TimedEvent",0);
        }

    }

    private IEnumerator TimedEvent(int start) 
    {
        for (int a = start; a < _numbers.Length; a++)
        {
            BstGenerator(_root,_numbers[a]);
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    private void BstGenerator(GameObject iter,int number)
    {
        var iterCont = iter.GetComponent<NodeController>();
        
        if (number >=  iterCont.value)
        {
            if (iterCont.rightChild==null)
            {
                iterCont.AddChild(true,number);
            }
            else
            {
                BstGenerator(iterCont.rightChild,number);
            }
            
        }
        
        else
        {
            if (iterCont.leftChild==null)
            {
                iterCont.AddChild(false,number);
            }
            else
            {
                BstGenerator(iterCont.leftChild,number);
                
            }
            
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
