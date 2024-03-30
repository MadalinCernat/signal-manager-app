// SignalDetailsPage.js
import React from 'react';
import { View, Text } from 'react-native';

const SignalDetailsPage = ({ route }) => {
  const { signal } = route.params;

  return (
    <View>
      <Text>Name: {signal.name}</Text>
      <Text>Symbol: {signal.symbol}</Text>
      {/*add info about the signal and buy sell buttons*/}
    </View>
  );
};

export default SignalDetailsPage;
