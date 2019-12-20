import { Injectable } from '@angular/core';
import {
  EpRenderMode, IEpViewModel, IEpContextBarItemModel, IEpUserContextMenu,
  IEpDropdownButtonItem, EpPosition, EpContextBarItemType
} from '@epicor/kinetic';
import { AppLocalizationStrings } from './app.localization.strings';
import { LeftSidebarComponent } from './left-sidebar/left-sidebar.component';

@Injectable()
export class AppCommonService {
  appStrings = AppLocalizationStrings;

  constructor() { }

  /**
   * Primary, reusable, view model to be used on every view.  This app does this
   * because it is the same menu for every screen.
   */
  getViewModel(): IEpViewModel {

    const userModel = {
      email: 'jdoe@epicor.com',
      data: [
        {
          id: 'account',
          caption: 'My Account',
          description: 'Edit account information',
          icon: 'mdi mdi-account',
          action: (menu: IEpUserContextMenu, menuItem: IEpDropdownButtonItem) => {
            alert(`This is the account for ${menu.userName}`);
          }
        },
        {
          id: 'settings',
          caption: 'Settings',
          description: 'ERP server settings',
          icon: 'mdi mdi-settings',
          action: (menu: IEpUserContextMenu, menuItem: IEpDropdownButtonItem) => {
            alert(`This is the account for ${menu.userName}`);
          }
        },
        {
          id: 'logout',
          caption: 'Sign Out',
          description: 'Exit system',
          icon: 'mdi mdi-power',
          action: (menu: IEpUserContextMenu, menuItem: IEpDropdownButtonItem) => {
            alert(`Logging out ${menu.userName}`);
          }
        }
      ]
    };

    const userContextItem: IEpContextBarItemModel = {
      id: 'user-context-menu',
      position: EpPosition.Right,
      type: EpContextBarItemType.UserContextMenu,
      controlModel: userModel
    };

    const viewModel: IEpViewModel = {
      contextBar: {
        title: 'Home',
        items: [
          userContextItem
        ]
      },
      footer: {
        textLeft: 'Epicor Software Corp.',
        textRight: ''
      },
      sidebarLeft: {
        component: LeftSidebarComponent,
        mode: EpRenderMode.Auto
      }
    };

    return viewModel;
  }
}
