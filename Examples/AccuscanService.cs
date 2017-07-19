using DG.Tweening;
using UnityEngine;

public class AccuscanService : PhysicalProductAnimator {
    public Animator mainAnimator;
    public ScrewAnimator bolts;
    //public Vector3 bolts;

    public override void StartService() {
        base.StartService();
        mainAnimator.enabled = false;
        bolts.AnimateIn(0);
        foreach(var l in physicalProduct.GetComponentsInChildren<RealtimeLaserEmmiter>()) { l.enabled = false; }
    }
    public override void EndService() {
        base.EndService();
        mainAnimator.enabled = true;
        bolts.AnimateOut(0);
        foreach (var l in physicalProduct.GetComponentsInChildren<RealtimeLaserEmmiter>()) { l.enabled = true; }
    }


    protected override void Start() {
        base.Start();
        //top layer
        physicalProduct.justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
            //Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
            if (down) {//reset object
                resetProduct();
            }
            else {
                genTween(physicalProduct.parts[4], AxisConstraint.Y, 6, .75f);
                genTween(physicalProduct.parts[5], AxisConstraint.Y, 6, .75f);
                //forward
                genTween(physicalProduct.parts[4], AxisConstraint.Z, 1, .5f,.75f);
                genTween(physicalProduct.parts[5], AxisConstraint.Z, 2, .5f,.75f);
                //down
                genTween(physicalProduct.parts[4], AxisConstraint.Y, 0.01f,.5f, 1.25f);
                genTween(physicalProduct.parts[5], AxisConstraint.Y, 0.01f,.5f, 1.25f);
                //back
                genTween(physicalProduct.parts[4], AxisConstraint.Z, 0.48f, .5f,1.6f);
                genTween(physicalProduct.parts[5], AxisConstraint.Z, 0, .65f,1.5f);
            }
        };
        //main housing
        physicalProduct.parts[1].justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { temp(down); };
        physicalProduct.parts[2].justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { temp(down); };
        physicalProduct.parts[3].justMovedToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { temp(down); };
    }
    private void temp(bool d) {
        if (d) { 
            //plate/screws forward
            genTween(physicalProduct.parts[4], AxisConstraint.Z, 1, .65f, .2f);
            genTween(physicalProduct.parts[5], AxisConstraint.Z, 2, .5f);
            //plate/screws up
            genTween(physicalProduct.parts[4], AxisConstraint.Y, 6, .5f, .75f);
            genTween(physicalProduct.parts[5], AxisConstraint.Y, 6, .5f, .75f);
            //plate/screws back 
            genTween(physicalProduct.parts[4], AxisConstraint.Z, -2, .5f, 1.25f);
            genTween(physicalProduct.parts[5], AxisConstraint.Z, -1, .5f, 1.25f);
            //down 
            genTween(physicalProduct.parts[4], AxisConstraint.Y, 0, .5f, 1.75f);
            genTween(physicalProduct.parts[5], AxisConstraint.Y, 0, .5f, 1.75f);
        }
        else {

        }
    }
}
