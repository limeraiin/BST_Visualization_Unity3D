using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField]private TextMeshPro valueText;
    public GameObject glow;
    public GameObject leftChild=null;
    public GameObject rightChild=null;
    public int value=0;

    private void Awake()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        glow.SetActive(true);
        yield return  new WaitForSecondsRealtime(0.2f);
        glow.SetActive(false);
    }


    public void AddChild(bool side, int childValue) //false: left, true: right
    {
        GameObject child;
        if(side==false)
        {
            GameObject target = (GameObject)Resources.Load("LeftNode");
            Set(target);
            leftChild = child;
        }
        else
        {
            GameObject target = (GameObject)Resources.Load("RightNode");
            Set(target);
            rightChild = child;
        }

        
        void Set(GameObject target)
        {
            child = Instantiate(target, transform.GetChild(1).transform.position,
                target.transform.rotation);
            child.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            child.GetComponent<NodeController>().SetValue(childValue);
        }
    }

    public void SetValue(int a)
    {
        value = a;
        valueText.text = a.ToString();
    }
}
