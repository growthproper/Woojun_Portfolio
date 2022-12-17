using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public void oneSkill();
    public void twoSkill();
    

}

public class Character : MonoBehaviour, ISkill
{
    virtual public void oneSkill()
    {

    }
    virtual public void twoSkill()
    {

    }


}
