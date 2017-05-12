/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */

import React, { Component } from 'react';
import {
  Alert,
  AppRegistry,
  StyleSheet,
  Text,
  View
} from 'react-native';

import Menu from '@bluejeans/react-native-wpf-sysmenu'

class WpfSysMenu extends Component {
  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.welcome}>
          Welcome to React Native!
        </Text>
        <Text style={{color:'blue', marginBottom:10}}>
          Demo of WPF application with extended system menu. To see menu
          press Alr+Spacebar.
        </Text>
        <Text style={styles.instructions}>
          To get started, edit index.windows.js
        </Text>
        <Text style={styles.instructions}>
          Press Ctrl+R to reload,{'\n'}
          Shift+F10 or shake for dev menu
        </Text>
      </View>
    );
  }
}


const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF',
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5,
  },
});

AppRegistry.registerComponent('SysMenu', () => WpfSysMenu);

const menu = Menu.create();

menu.addSeparator()
  .then(() => menu.addItem(1, 'Test Item 1', () => Alert.alert('Clicked', 'Item 1')))
  .then(() => menu.addItem(2, 'Test Item 2', () => Alert.alert('Clicked', 'Item 2')))
  .then(() => menu.addItem(3, 'Test Item 3', () => Alert.alert('Clicked', 'Item 3')))
  .then(() => menu.enableItem(2, false))
  .catch((e) => Alert.alert('Error', e.message))
