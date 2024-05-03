import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import LogBinance from './LogBinance';
import SignalListPage from './SignalListPage';
import SignalDetailsPage from './SignalDetailsPage';

const Stack = createStackNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="SignalList">
        <Stack.Screen name="Log" component={LogBinance} />
        <Stack.Screen name="SignalList" component={SignalListPage} />
        <Stack.Screen name="SignalDetails" component={SignalDetailsPage} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
