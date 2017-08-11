using DG.Tweening;
using UnityEngine;

public class AccuscanService : PhysicalProductAnimator {

	public PhysicalPart LaserEmitters;
	public void ShowService() {
		Transform boltsParent = physicalProduct.parts[5].transform;
		Transform[] bolts = boltsParent.GetChildrenArray();
		for (int i = 0; i < bolts.Length; i++) { bolts[i].DOLocalRotate(new Vector3(0, 0,720), 1f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);  }
		boltsParent.DOLocalMoveZ(2, 1).SetEase(Ease.InOutCubic).OnComplete(()=> { boltsParent.gameObject.SetActive(false); });
		physicalProduct.parts[4].transform.DOLocalMoveX(-6, .5f).SetDelay(.25f).SetEase(Ease.InCubic);
		physicalProduct.parts[4].transform.DOLocalMoveX(0, 1f).SetDelay(.75f).SetEase(Ease.OutCubic);
		physicalProduct.parts[4].transform.DOLocalRotate(new Vector3(0, -180, 0), .5f).SetDelay(.75f);
		physicalProduct.parts[4].transform.DOLocalMoveZ(1, .5f).SetDelay(.25f).SetEase(Ease.OutCubic);
		physicalProduct.parts[4].transform.DOLocalMoveZ(-1, .75f).SetDelay(.5f).SetEase(Ease.InOutCubic);
	}
	public void HideService() { 
		Transform boltsParent = physicalProduct.parts[5].transform;
		Transform[] bolts = boltsParent.GetChildrenArray();
		boltsParent.gameObject.SetActive(true);
		for (int i = 0; i < bolts.Length; i++) { bolts[i].DOLocalRotate(new Vector3(0, 0, 720), 1f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic); }
		physicalProduct.parts[4].transform.DOLocalMoveX(-6, .5f).SetEase(Ease.InCubic);
		physicalProduct.parts[4].transform.DOLocalMoveX(0, .5f).SetDelay(.5f).SetEase(Ease.OutCubic);
		physicalProduct.parts[4].transform.DOLocalRotate(new Vector3(0, 0, 0), .75f).SetDelay(.15f);
		physicalProduct.parts[4].transform.DOLocalMoveZ(0.48f, .75f).SetDelay(.25f).SetEase(Ease.InOutSine);
		boltsParent.DOLocalMoveZ(0, .5f).SetDelay(1).SetEase(Ease.InOutCubic);
	}


    protected override void Start() {
        base.Start();
        //top layer
        physicalProduct.animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) {
			LaserEmitters.gameObject.SetActive(true);
			if (down) { resetProduct(); }
            else { }
        };
		physicalProduct.jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { 
			if (down) { resetProduct(); }
			else {
				physicalProduct.parts[4].transform.localPosition = physicalProduct.parts[4].originalPosition.position;
				physicalProduct.parts[5].transform.localPosition = physicalProduct.parts[5].originalPosition.position;
			}
		}; 
		//physicalProduct.parts[1].animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { standardAnim(down); };
        //physicalProduct.parts[2].animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { standardAnim(down); };
        //physicalProduct.parts[3].animateToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { standardAnim(down); };

		physicalProduct.parts[1].jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { standardJump(down); };
		physicalProduct.parts[2].jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { standardJump(down); };
		physicalProduct.parts[3].jumpToThisLayer += delegate (PhysicalSceneObject THIS, bool down) { standardJump(down); };
	}
    private void standardAnim(bool d) {
        if (d) {
			Transform boltsParent = physicalProduct.parts[5].transform;
			Transform[] bolts = boltsParent.GetChildrenArray();
			for (int i = 0; i < bolts.Length; i++) { bolts[i].DOLocalRotate(new Vector3(0, 0, 720), 1f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic); }
			boltsParent.DOLocalMoveZ(2, 1).SetEase(Ease.InOutCubic).OnComplete(() => { boltsParent.gameObject.SetActive(false); });
			physicalProduct.parts[4].transform.DOLocalMoveX(-6, .5f).SetDelay(.25f).SetEase(Ease.InCubic);
			physicalProduct.parts[4].transform.DOLocalMoveX(0, 1f).SetDelay(.75f).SetEase(Ease.OutCubic);
			physicalProduct.parts[4].transform.DOLocalRotate(new Vector3(0, -180, 0), .5f).SetDelay(.75f);
			physicalProduct.parts[4].transform.DOLocalMoveZ(1, .5f).SetDelay(.25f).SetEase(Ease.OutCubic);
			physicalProduct.parts[4].transform.DOLocalMoveZ(-1, .75f).SetDelay(.5f).SetEase(Ease.InOutCubic);
			LaserEmitters.gameObject.SetActive(false);
		} 
    }

	private void standardJump(bool d) {
		if (d) {
			physicalProduct.parts[4].transform.localPosition = new Vector3(physicalProduct.parts[4].transform.localPosition.x, 0, -2);
			physicalProduct.parts[5].transform.localPosition = new Vector3(physicalProduct.parts[5].transform.localPosition.x, 0, -1);

			LaserEmitters.gameObject.SetActive(false);
		}
	}
}
