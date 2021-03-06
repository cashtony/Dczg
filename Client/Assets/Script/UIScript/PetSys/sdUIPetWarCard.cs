﻿using UnityEngine;
using System.Collections;
using System;

public class sdUIPetWarCard : MonoBehaviour
{
	public UInt64 m_uuDBID = UInt64.MaxValue;
	public int index = -1;
	public int iOpenLevel = 0;

	public GameObject bg = null;
	public GameObject openLevel = null;
	public GameObject add = null;
	public GameObject bgColor = null;
	public GameObject icon = null;
	public GameObject txtLevel = null;
	public GameObject type = null;
	public GameObject star0 = null;//星星标志0..
	public GameObject star1	= null;//星星标志1..
	public GameObject star2	= null;//星星标志2..
	public GameObject star3	= null;//星星标志3..
	public GameObject star4	= null;//星星标志4..
	public GameObject txtName = null;//宠物名字..
	public GameObject up = null;//强化等级..
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	void OnClick()
	{
		if (gameObject)
		{
			if (m_uuDBID!=UInt64.MaxValue)
			{
				GameObject wnd = sdGameLevel.instance.NGUIRoot;
				if (wnd)
				{
					sdUIPetWarPnl petWarPnl = wnd.GetComponentInChildren<sdUIPetWarPnl>();
					if (petWarPnl)
					{
						if (iOpenLevel>0)
						{
							int iLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
							if (iLevel>=iOpenLevel)
							{
								sdUIPetControl.Instance.ActivePetWarSelectPnl(null, m_uuDBID, petWarPnl.mCurTeamIndex*7+index);
							}
							else
							{
								string strTemp = string.Format("{0}级后开放该助战位", iOpenLevel);
								sdUICharacter.Instance.ShowMsgLine(strTemp, MSGCOLOR.Yellow);
							}
						}
						else
						{
							sdUIPetControl.Instance.ActivePetWarSelectPnl(null, m_uuDBID, petWarPnl.mCurTeamIndex*7+index);
						}
					}
				}
			}
		}
	}
	
	public void ReflashPetIconUI(UInt64 uuID)
	{
		if (uuID==UInt64.MaxValue) 
		{
			m_uuDBID = 0;
			gameObject.SetActive(true);

			if (iOpenLevel>0)
			{
				int iLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
				if (iLevel>=iOpenLevel)
				{
					if (bg)
						bg.SetActive(true);
					
					if (openLevel)
						openLevel.SetActive(false);
					
					if (add)
						add.SetActive(true);
					
					if (bgColor)
						bgColor.SetActive(false);
				}
				else
				{
					if (bg)
						bg.SetActive(true);
					
					if (openLevel)
						openLevel.SetActive(true);
					
					if (add)
						add.SetActive(false);
					
					if (bgColor)
						bgColor.SetActive(false);
				}
			}
			else
			{
				if (bg)
					bg.SetActive(true);

				if (openLevel)
					openLevel.SetActive(false);
				
				if (add)
					add.SetActive(true);
				
				if (bgColor)
					bgColor.SetActive(false);
			}

			return;
		}

		m_uuDBID = uuID;
		gameObject.SetActive(true);

		if (bg)
			bg.SetActive(false);

		if (openLevel)
			openLevel.SetActive(false);

		if (add)
			add.SetActive(false);

		if (bgColor)
			bgColor.SetActive(true);
		
		SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;

		if (icon)
			icon.GetComponent<UISprite>().spriteName = Info.m_strIcon;

		if (txtLevel)
			txtLevel.GetComponent<UILabel>().text = Info.m_iLevel.ToString();

		if (type)
		{
			if (Info.m_iBaseJob==1)
			{
				type.GetComponent<UISprite>().spriteName = "IPzs";
				type.SetActive(true);
			}
			else if (Info.m_iBaseJob==2)
			{
				type.GetComponent<UISprite>().spriteName = "IPfs";
				type.SetActive(true);
			}
			else if (Info.m_iBaseJob==3)
			{
				type.GetComponent<UISprite>().spriteName = "IPyx";
				type.SetActive(true);
			}
			else if (Info.m_iBaseJob==4)
			{
				type.GetComponent<UISprite>().spriteName = "IPms";
				type.SetActive(true);
			}
			else
			{
				type.SetActive(false);
			}
		}

		SetPetStar(Info.m_iAbility);
		
		if (txtName)
		{
			sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, txtName);
			txtName.GetComponent<UILabel>().text = Info.m_strName;
		}
		
		if (up)
		{
			if (Info.m_iUp==1)
			{
				up.GetComponent<UISprite>().spriteName = "pet_a1";
				up.SetActive(true);
			}
			else if (Info.m_iUp==2)
			{
				up.GetComponent<UISprite>().spriteName = "pet_a2";
				up.SetActive(true);
			}
			else if (Info.m_iUp==3)
			{
				up.GetComponent<UISprite>().spriteName = "pet_a3";
				up.SetActive(true);
			}
			else if (Info.m_iUp==4)
			{
				up.GetComponent<UISprite>().spriteName = "pet_a4";
				up.SetActive(true);
			}
			else if (Info.m_iUp==5)
			{
				up.GetComponent<UISprite>().spriteName = "pet_a5";
				up.SetActive(true);
			}
			else
			{
				up.SetActive(false);
			}
		}
	}
	
	public void SetPetStar(int iStar)
	{
		//底板颜色..
		if (bgColor)
		{
			string strPicName = string.Format("petclass{0}", iStar);
			bgColor.GetComponent<UISprite>().spriteName = strPicName;
			bgColor.SetActive(true);
		}

		//星级..
		if (star0==null || star1==null || star2==null || star3==null || star4==null)
			return;
		
		float fWidth = (float)star0.GetComponent<UISprite>().width*1.0f;
		
		if (iStar==1)
		{
			star0.SetActive(false);
			star1.SetActive(false);
			star2.SetActive(true);
			star3.SetActive(false);
			star4.SetActive(false); 
			
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			star0.SetActive(false);
			star1.SetActive(false);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(false);
			
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			star0.SetActive(false);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(false);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			star0.SetActive(false);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(true);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
			star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, star4.GetComponent<UISprite>().transform.localPosition.y, 
					star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			star0.SetActive(true);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(true);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
			star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, star4.GetComponent<UISprite>().transform.localPosition.y, 
					star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			star0.SetActive(true);
			star1.SetActive(false);
			star2.SetActive(false);
			star3.SetActive(false);
			star4.SetActive(false);
		}
	}
}