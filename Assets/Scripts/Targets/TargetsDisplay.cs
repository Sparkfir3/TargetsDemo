using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsDisplay : MonoBehaviour{
    
    public GameObject targetIcon;

    private Stack<GameObject> iconList = new Stack<GameObject>();
    private int iconCount = 0;
    
    private void LateUpdate() {
        if(iconCount < TargetManager.count) {
            AddIcon();
        } else if(iconCount > TargetManager.count && iconCount > 0) {
            RemoveIcon();
        }
    }

    private void AddIcon() {
        GameObject newTarget = Instantiate(targetIcon, transform);
        newTarget.GetComponent<RectTransform>().localPosition = GetNewTargetPos();
        iconCount++;
        iconList.Push(newTarget);
    }

    private Vector3 GetNewTargetPos() {
        Vector3 pos = new Vector3(5, -5, 0);
        for(int i = iconCount; i > 4; i -= 5)
            pos.y += -30;
        for(int i = iconCount % 5; i > 0; i--)
            pos.x += 30;
        return pos;
    }

    private void RemoveIcon() {
        GameObject target = iconList.Pop();
        Destroy(target);
        iconCount--;
    }

}
