# react-native-wpf-sysmenu
An extension of the Windows system menu with custom items.

## Designe notes
 - Control assumes that there is no any other components that can modify system menu.
 - Won't check for ID duplicates, just overwrite it.
 - By some reasons _EventEmitter_ is included by relative path `../../react-native/Libraries/EventEmitter/RCTDeviceEventEmitter`, in case if in app file will be in different location - the path should be changed appropriately
 - Id should be in range 1 - 61400

 ## Example
 Full example can be ssen at `Example` folder.

 ```javascript
import Menu from '@bluejeans/react-native-wpf-sysmenu'

const menu = Menu.create();

menu.addSeparator()
  .then(() => menu.addItem(1, 'Test Item 1', () => Alert.alert('Clicked', 'Item 1')))
  .then(() => menu.addItem(2, 'Test Item 2', () => Alert.alert('Clicked', 'Item 2')))
  .then(() => menu.addItem(3, 'Test Item 3', () => Alert.alert('Clicked', 'Item 3')))
  .then(() => menu.enableItem(2, false))
  .catch((e) => Alert.alert('Error', e.message))
 ```