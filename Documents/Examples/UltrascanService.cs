using DG.Tweening;
using UnityEngine;
 
public class UltrascanService : PhysicalProductAnimator {

    protected override void Start() {
        base.Start();
        //top layer
        physicalProduct.animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
            //Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
            if (down) { resetProduct(); }
            else {
                genTween(physicalProduct.parts[2], AxisConstraint.Z, 0, .5f);
                genTween(physicalProduct.parts[1], AxisConstraint.Z, 0, .5f);
                genTween(physicalProduct.parts[2], AxisConstraint.Y, 0, .5f,.6f);
                genTween(physicalProduct.parts[1], AxisConstraint.Y, 0f, .5f, .5f);
                resetProduct();
            }
        };

		physicalProduct.jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) { resetProduct(); }
			else {
				physicalProduct.parts[2].transform.localPosition = physicalProduct.parts[2].transform.localPosition = Vector3.zero;
				resetProduct();
			}
		};

		// circuit and screen
		physicalProduct.parts[3].animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
            //Debug.Log("The first part in PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
            if (down) {
                genTween(physicalProduct.parts[2], AxisConstraint.Y, -.05f,.5f);
                genTween(physicalProduct.parts[1], AxisConstraint.Y, -.03f,.4f,.1f);
                genTween( physicalProduct.parts[2], AxisConstraint.Z, .1f, .5f, .5f);
                genTween(physicalProduct.parts[1], AxisConstraint.Z, .1f, .5f, .5f);
                centerObject(physicalProduct.parts[3], 1);
            }
        };
		physicalProduct.parts[3].jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("The first part in PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) {
				physicalProduct.parts[1].transform.localPosition = new Vector3(physicalProduct.parts[1].transform.localPosition.x, -.03f, .1f);
				physicalProduct.parts[2].transform.localPosition = new Vector3(physicalProduct.parts[2].transform.localPosition.x, -.05f, .1f);
				centerObject(physicalProduct.parts[3], 1, false);
			}
		};

		physicalProduct.parts[4].animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
            //Debug.Log("The first part in PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
            if (down) {
                genTween(physicalProduct.parts[2], AxisConstraint.Y, -.05f, .5f);
                genTween(physicalProduct.parts[1], AxisConstraint.Y, -.03f, .4f, .1f);
                genTween(physicalProduct.parts[2], AxisConstraint.Z, .1f, .5f, .5f);
                genTween(physicalProduct.parts[1], AxisConstraint.Z, .1f, .5f, .5f);
            }
        };
		physicalProduct.parts[4].jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			//Debug.Log("The first part in PhysicialProduct is the new current layer. Animate accordingly.  " + (down ? "Came Down" : "Went Up"));
			if (down) {
				physicalProduct.parts[1].transform.localPosition = new Vector3(physicalProduct.parts[1].transform.localPosition.x, -.03f, .1f);
				physicalProduct.parts[2].transform.localPosition = new Vector3(physicalProduct.parts[2].transform.localPosition.x, -.05f, .1f);
			}
		};

	}
    public void removeBolts() { steps.Add(new Step(physicalProduct.parts[2],genTween(physicalProduct.parts[2], AxisConstraint.Y, -.05f))); }
    public void removePlate() { steps.Add(new Step(physicalProduct.parts[1],genTween(physicalProduct.parts[1], AxisConstraint.Y, -.03f))); }

    #region screen (parts[3])
    public void removeScreenBracket() { steps.Add(new Step(physicalProduct.parts[3].parts[0],genTween(physicalProduct.parts[3].parts[0], AxisConstraint.Z, .1f))); }
    public void removeRoundScreen() {
        physicalProduct.parts[3].parts[0].populateStep.Invoke();
        steps.Add(new Step(physicalProduct.parts[3].parts[1],genTween(physicalProduct.parts[3].parts[1], AxisConstraint.Z, .1f)));
    }
    public void removeCornerBracket() { steps.Add(new Step(physicalProduct.parts[3].parts[2], genTween(physicalProduct.parts[3].parts[2], AxisConstraint.Z, .1f))); }
    public void removeSpacer() {
        steps.Add(new Step(physicalProduct.parts[3].parts[3], new Tween[] {
            genTween(physicalProduct.parts[3].parts[3], AxisConstraint.Y, .01f),
            genTween(physicalProduct.parts[3].parts[3], AxisConstraint.Z, .1f)
        }));
    }
    #endregion
    #region circuit (parts[4])
    public void removeAuxCircuit() { steps.Add(new Step(physicalProduct.parts[4].parts[0],genTween(physicalProduct.parts[4].parts[0], AxisConstraint.Y, -.1f))); }
    public void removeMainCircuit() { steps.Add(new Step(physicalProduct.parts[4].parts[1],genTween(physicalProduct.parts[4].parts[1], AxisConstraint.Y, -.1f))); }
    #endregion
}
