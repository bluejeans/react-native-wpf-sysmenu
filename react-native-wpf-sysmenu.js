/* @flow */
import { NativeModules } from 'react-native'
import RCTDeviceEventEmitter from '../../react-native/Libraries/EventEmitter/RCTDeviceEventEmitter'

const _menu = NativeModules.WpfSysMenuComponent

const create = () => {
  let handlers = []

  const addSeparator = () => _menu.addSeparator()
  const addItem = (id, name, handler) => {
    handlers.push({id, handler})
    return _menu.addItem(id, name)
  }
  const enableItem = (id, isEnabled) => _menu.enableItem(id, isEnabled)
  const removeAll = () => {
    handlers = []
    return _menu.removeAll()
  }

  RCTDeviceEventEmitter.addListener('SystemMenuItemClicked', (id) => handlers.forEach((i) => {
    if (i.id === id && i.handler) { i.handler() }
  }))

  return {
    addSeparator,
    addItem,
    enableItem,
    removeAll
  }
}

export default {
  create
}
