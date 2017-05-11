/* @flow */
import { NativeModules, Alert } from 'react-native'

const _menu = NativeModules.WpfSysMenuComponent

const create = () => {
  const addSeparator = () => _menu.addSeparator()
  const addItem = (id, name) => _menu.addItem(id, name)
  const enableItem = (id, isEnabled) => _menu.enableItem(id, isEnabled)
  const removeAll = () => _menu.removeAll()

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
