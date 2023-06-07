// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "PhoenixCrossGameModeBase.generated.h"

/**
 * 
 */
UCLASS()
class PHOENIXCROSS_API APhoenixCrossGameModeBase : public AGameModeBase
{
	GENERATED_BODY()


protected:
	virtual void BeginPlay() override;
};
