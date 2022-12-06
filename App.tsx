import React, {ComponentProps} from 'react';
import {useWindowDimensions} from 'react-native';
import {NavigationContainer} from '@react-navigation/native';
import HomeScreen from './components/HomeScreen';
import winuiNavigatorFactory from './winuiNavigationViewNavigator';
import {FontIcon} from 'react-native-xaml';

const NavigatioinView = winuiNavigatorFactory();

const App = () => {
  const {height, width} = useWindowDimensions();

  type props = ComponentProps<typeof NavigatioinView.Screen>;

  return (
    <NavigationContainer>
      <NavigatioinView.Navigator style={{height}}>
        <NavigatioinView.Screen
          name="Home"
          component={HomeScreen}
          options={{children: <FontIcon glyph="&#xE80F;" />}}
        />
        <NavigatioinView.Screen
          name="Devices"
          component={HomeScreen}
          options={{children: <FontIcon glyph="&#xE772;" />}}
        />
      </NavigatioinView.Navigator>
    </NavigationContainer>
  );
};

export default App;
