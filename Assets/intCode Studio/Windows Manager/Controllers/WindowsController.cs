﻿// --------------------------------------------------------------
// WindowsController.cs is part of the VLab project.
// Copyright (c) 2016 All Rights Reserved
// Li Alex Zhang fff008@gmail.com
// 5-9-2016
// --------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowsController : MonoBehaviour
{
    [SerializeField]
    private PopupController popupController;

    [SerializeField]
    private bool navigationHistory = true;

    private List<IWindow> openedWindows;

    public static WindowsController Instance
    {
        get;
        private set;
    }

    public PopupController PopupController
    {
        get { return popupController; }
    }

    public bool NavigationHistory
    {
        get { return navigationHistory; }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Windows Controller is already exist.");

        popupController.Setup(FindObjectOfType<UIPopup>(), this);
    }

    private void Start()
    {
        openedWindows = new List<IWindow>();
    }

    public void RegisterWindow(IWindow window)
    {
        HideLastWindow();
        openedWindows.Add(window);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            ReturnToLastWindow();
    }

    public void ReturnToLastWindow()
    {
        if(navigationHistory && openedWindows.Count > 1)
        {
            RemoveWindow();
            openedWindows[openedWindows.Count - 1].Unhide();
        }
        else if(navigationHistory && openedWindows.Count > 0)
            RemoveWindow();
    }

    private void RemoveWindow()
    {
        IWindow window = openedWindows[openedWindows.Count - 1];
        window.Hide();
        openedWindows.Remove(window);
    }

    public void UnhideLastWindow()
    {
        if (openedWindows.Count > 0)
            openedWindows[openedWindows.Count - 1].Unhide();
    }

    public void HideLastWindow()
    {
        if(openedWindows.Count > 0)
            openedWindows[openedWindows.Count - 1].Hide();
    }

    public void DeleteNavigationHistory()
    {
        openedWindows.Clear();
    }
}