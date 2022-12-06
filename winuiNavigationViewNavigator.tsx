/* eslint-disable react-native/no-inline-styles */
/* eslint-disable @typescript-eslint/no-unused-vars */

import React, {type ComponentProps, type FC} from 'react';

import {View, Text, useWindowDimensions} from 'react-native';

import {
  Grid,
  Frame,
  NavigationView,
  NavigationViewItem,
  NavigationViewPriority,
  StackPanel,
} from 'react-native-xaml';

import {
  createNavigatorFactory,
  useNavigationBuilder,
  DrawerRouter,
  CommonActions,
  type DrawerNavigationState,
  type DefaultNavigatorOptions,
  type ParamListBase,
  type DrawerRouterOptions,
  type DrawerActionHelpers,
} from '@react-navigation/native';
import {SafeAreaProviderCompat} from '@react-navigation/elements';

type WinUINavigationConfig = Omit<
  ComponentProps<typeof NavigationView>,
  'children'
>;

type WinUiNavifationViewItemProps = ComponentProps<typeof NavigationViewItem>;
type WinUINavigationOptions = WinUiNavifationViewItemProps;

type WinUINavigationEventMap = {};

type WinUINavigationNavigatorProps = DefaultNavigatorOptions<
  ParamListBase,
  DrawerNavigationState<ParamListBase>,
  WinUINavigationOptions,
  WinUINavigationEventMap
> &
  DrawerRouterOptions &
  WinUINavigationConfig;

const WinUINavigationNavigator: FC<WinUINavigationNavigatorProps> = ({
  initialRouteName,
  children,
  screenOptions,
  ...rest
}) => {
  const {height, width} = useWindowDimensions();
  const {state, navigation, descriptors, NavigationContent} =
    useNavigationBuilder<
      DrawerNavigationState<ParamListBase>,
      DrawerRouterOptions,
      DrawerActionHelpers<ParamListBase>,
      WinUINavigationOptions,
      WinUINavigationEventMap
    >(DrawerRouter, {
      children,
      screenOptions,
      initialRouteName,
    });

  return (
    <SafeAreaProviderCompat>
      <NavigationContent>
        <Grid>
          <NavigationView {...rest}>
            {state.routes.map(route => (
              <NavigationViewItem
                key={route.key}
                content={descriptors[route.key].options.content || route.name}
                {...descriptors[route.key].options}
                onTapped={e => {
                  navigation.dispatch({
                    ...CommonActions.navigate({
                      name: route.name,
                      merge: true,
                    }),
                    target: state.key,
                  });
                }}
              />
            ))}
            <StackPanel
              priority={NavigationViewPriority.Content}
              style={[{flex: 1, alignItems: 'stretch'}]}>
              {/* {state.routes.map((route, i) => (
                <Text
                  key={route.key}
                  style={[{display: i === state.index ? 'flex' : 'none'}]}>
                  Route: {route.name}
                </Text>
              ))} */}
              {state.routes.map((route, i) => (
                <View
                  key={route.key}
                  style={[
                    {
                      flex: 1,
                      backgroundColor: 'blue',
                      height: '100%',
                      width: '100%',
                    },
                    {display: i === state.index ? 'flex' : 'none'},
                  ]}>
                  <Text>Route View: {route.name}</Text>
                </View>
              ))}
            </StackPanel>
          </NavigationView>
        </Grid>
      </NavigationContent>
    </SafeAreaProviderCompat>
  );
};

export default createNavigatorFactory<
  DrawerNavigationState<ParamListBase>,
  WinUINavigationOptions,
  WinUINavigationEventMap,
  typeof WinUINavigationNavigator
>(WinUINavigationNavigator);
